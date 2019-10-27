Метод чтения плана read:
try{ 
	InputStream ExcelFileToRead = new FileInputStream(path);
    XSSFWorkbook wb = new XSSFWorkbook(ExcelFileToRead);
    XSSFSheet sheet0 = wb.getSheetAt(0); //страница с расписанием ТСЕ
    XSSFSheet sheet1 = wb.getSheetAt(1); //страница с расписанием режимов работы станций
    XSSFSheet sheet2 = wb.getSheetAt(2); //страница со связями
    XSSFSheet sheet3 = wb.getSheetAt(3); //страница со списком станций и дублёров
    
    readStations(wb, sheet3);
    readPlan(wb, sheet0);
    readSchedule(wb, sheet1);
    readConnections(wb, sheet2);
}
catch(IOException ex){}  

Метод чтения списка станций readStations:

	XSSFRow row;
	XSSFCell cell;
	Iterator rows = sheet.rowIterator();
	DataFormatter formatter=new DataFormatter();
	FormulaEvaluator evaluator=new XSSFFormulaEvaluator(wb);
	boolean first=true;
	boolean isAlternate=false;
	boolean notAllStations=false;
	String strAlternate;
	String name;
	String typeName="";
	int count=0;	//сколько станций одного типа уже
    Date dateInput;
	row = (XSSFRow) rows.next();
    while (rows.hasNext()) {
    	dateInput=null;
    	isAlternate=false;
        row = (XSSFRow) rows.next();
        Iterator cells = row.cellIterator();
        cell = (XSSFCell) cells.next();
        evaluator.evaluate(cell);
    	name=formatter.formatCellValue(cell, evaluator);
        name=name.trim(); 
        name=name.replaceAll("[\\s]{2,}", " ");//удаление повторяющихся пробелов внутри строки
        name=russianToEnglish(name);
        
        StringBuilder sb=new StringBuilder(name);
        int ind=name.indexOf(" ");
        if(ind==-1) ind=name.length();
        String ns=name.split(" ")[0];
        name = getNewNameStation(name);
        
        if(!name.equals("")){
	        cell = (XSSFCell) cells.next();
	        evaluator.evaluate(cell);
	   	 	strAlternate=formatter.formatCellValue(cell, evaluator);
	        strAlternate=strAlternate.trim();
	       String [] mass=name.split(" ");
	       if(mass[mass.length-1].charAt(0)=='(')isAlternate=true; //временно определяю дублёра по индексу в скобочках
	       else isAlternate=false;
	       
	        //дублёра обрабатывает по-особому. Т.к. для оригинальной станции есть агент, а дублёр "размещается" на существующем агенте и для него изначально нет объекта
	        
	    	String[] mas=name.split(" ");
	    	typeName="";
	    	
	    	 
	         //typeName=mas[mas.length-2]; 
	         if(isAlternate){
		         for(int i=0;i<=mas.length-2;i++)//берём всё, кроме цифры в скобочках, которая есть только у дублёров
		    		typeName=typeName+" "+mas[i];
		         String s=mas[mas.length-1].replace('(', ' '); //вырезание скобочек
		         s=s.replace(')', ' ');
		         s=s.trim();
		         count=Integer.parseInt(s);
	         }
	         else typeName=name;
	         typeName=typeName.trim();
	    	 typeName=russianToEnglish(typeName);
	        cell = (XSSFCell) cells.next();
	        evaluator.evaluate(cell);
	   	 	String date=formatter.formatCellValue(cell, evaluator);
	   	 	date=date.trim();
	   	 	dateInput=getCalendarFromStr(date).getTime();
	   	 	
	   	 	Station station=add_stations();
	   	 	station.name=name;
	   	 	station.type=typeName;
	   	 	station.parallelProcessing=false;
	   	 	station.alternates=false;
	   	 	station.nonBreaking=false;
	   	 	station.mainn=this;
	   	 	station.alternate=false;
	   	 	
	   	 	//замена точек, запятых, тире, скобочек, пробелов на _
	   	 	String nodeName=name.replaceAll("[.,-]", "_").replaceAll("\\)","_").replaceAll("\\(","_").replaceAll("\\s","_") ;
	   	 	nodeName="node"+nodeName;
	   	 	if(collectionNodes_Map.containsKey(nodeName)){
	   	 		station.setLocation(collectionNodes_Map.get(nodeName));
	   	 	}
	   	 	else{
	   	 		notAllStations=true;
	   	 		traceln("Нужно добавить для станции "+station.name+" узел с названием "+nodeName);   	 		
	   	 		
	   	 	}
	   	 	
	   	 	collectionStationWithAlternates.add(station);
	   	 	collectionStation_Map.put(name, station);
	   	 	if(!collectionType_Station.containsKey(typeName)){
				collectionType_Station.put(typeName, new ArrayList<Station>());
				collectionType_Station.get(typeName).add(station);
			}
			else collectionType_Station.get(typeName).add(station);
			
			if(!collectionTypes.contains(typeName)) collectionTypes.add(typeName);
		}
     }
     if(notAllStations)
     {
     	error("Пожалуйста, добавьте на планировку недостающие станции. Список станций выведен в консоль");
     }

Метод чтение расписания производства самолётов readPlan

try{ 
	FileWriter fw=new FileWriter("plan.txt");
	fw.write("");
	XSSFSheet sheet2 = wb.getSheetAt(2); //страница со связями
	XSSFRow row;
	XSSFCell cell;
   	Iterator rows = sheet.rowIterator();
   	DataFormatter formatter=new DataFormatter();
   	FormulaEvaluator evaluator=new XSSFFormulaEvaluator(wb);
   	   	
    String name;
 	String durationStr;
    String dateStartStr;
	String dateEndStr;
	String processingStation = "";
 	String nextTCEString = "";
 	String stationAndRack="";
 	String rack;
 	Date dateStart;
	Date dateEnd;
	String airplaneNumb="";
	String typeName="";
    boolean last = false;
    double duration;
    int colorInd=0;
    ArrayList<String> airplanes=new ArrayList<String>();
    LinkedHashMap<String,String> tce_Station=new LinkedHashMap();
    rows.next();
    while (rows.hasNext()) {
    	rack="";
    	typeName="";
	    nextTCEString=null;
        row = (XSSFRow) rows.next();
        Iterator cells = row.cellIterator();
        cell = (XSSFCell) cells.next();
        //преобразование значения из любовго формата ячейки в строку
        evaluator.evaluate(cell);
        name=formatter.formatCellValue(cell, evaluator);
        name=name.trim();
        
        cell = (XSSFCell) cells.next();
        evaluator.evaluate(cell);
        durationStr=formatter.formatCellValue(cell, evaluator);
        durationStr=durationStr.replaceAll("\\p{Z}",""); //удаляем неразрывные пробелы посреди числа 
        durationStr=durationStr.replace(",",".");
        if (cell.getColumnIndex()==1){ //защита от пустой длительности
	        cell = (XSSFCell) cells.next();
	        evaluator.evaluate(cell);
	        dateStartStr=formatter.formatCellValue(cell, evaluator);
	        dateStartStr=dateStartStr.trim();
	        
	        cell = (XSSFCell) cells.next();
	        evaluator.evaluate(cell);
	        dateEndStr=formatter.formatCellValue(cell, evaluator);
	        dateEndStr=dateEndStr.trim();

	        cell = (XSSFCell) cells.next();
	  		if (cell.getColumnIndex()==4){
	  		  stationAndRack=cell.getStringCellValue().trim();
	  		  processingStation=stationAndRack.split(";")[0].trim();
	          if(stationAndRack.split(";").length>1)
	          	rack=stationAndRack.split(";")[1].trim();
	          processingStation = processingStation.replaceAll("[\\s]{2,}", " ");
	          processingStation = russianToEnglish(processingStation);
	          
	          processingStation = getNewNameStation(processingStation);
		        
	          String[] mas=processingStation.split(" ");

	          for(int i=0;i<=mas.length-2;i++)//берём всё, кроме цифры в скобочках, которая есть только у дублёров
	        	typeName=typeName+" "+mas[i];
	          typeName=typeName.trim();
	          typeName=russianToEnglish(typeName);
	        	if(!processingStation.equals(""))
	        	{
		        	cell = (XSSFCell) cells.next();
		        	//был план, где пропущено у некоторых ТСЕ ТСЕ-родитель. Поэтому проверка, существует ли следующая ячейка. Если не существует, значит,
		        	//в плане не было ТСЕ-родитель, и мы сейчас стоим на значении комплекта
		          	if(cells.hasNext()){
			          	evaluator.evaluate(cell);
		        		nextTCEString=formatter.formatCellValue(cell, evaluator);
			        	nextTCEString=nextTCEString.trim();
		          		cell = (XSSFCell) cells.next();
		          	}
		          	airplaneNumb = cell.getStringCellValue();
		          	airplaneNumb=airplaneNumb.trim();
	          	}
	          	else{
	          		nextTCEString=null;
		        	last=true;
	          	}
	        }
	        else
	        {
		        airplaneNumb = cell.getStringCellValue();
		        nextTCEString=null;
		        last=true;
	        }       
		    String key=name+"@"+airplaneNumb;
		    
		    if(!collectionTCE_Map.containsKey(key))
		    {       
			    duration = Double.parseDouble(durationStr);
			    if(!processingStation.equals("") && collectionStation_Map.containsKey(processingStation))
			   	 	collectionStation_Map.get(processingStation).planDurationOfWork+=duration;
	
				//получение даты и времени начала обработки
				dateStart = getCalendarFromStr(dateStartStr).getTime();

				//получение даты и времени конца обработки
			    dateEnd = getCalendarFromStr(dateEndStr).getTime();;
			    TCE t;
			    if(processingStation.equals("")) processingStation="F20";
			    //Игнорирование записей с неизвестными ресурсами
			    if(collectionStation_Map.containsKey(processingStation) || collectionTypes.contains(processingStation)
			    || collectionTypes.contains(typeName)){ 
					t = new TCE(dateStart, duration, collectionStation_Map.get(processingStation), null,null,name,airplaneNumb,dateEnd);
					if(!collectionStation_Map.containsKey(processingStation) && 
					collectionTypes.contains(processingStation)) t.stationType=processingStation;
					
					if(!collectionStation_Map.containsKey(processingStation) && 
					collectionTypes.contains(typeName)) t.stationType=typeName;
					t.nextTCEString=nextTCEString;
				    if(!rack.equals(""))
				    {
				    	t.rack=collectionRack_Map.get(rack);
				    }
				    
				    if(!airplanes.contains(t.airplane))
				   		airplanes.add(t.airplane);
				    collectionTCE.add(t);
				    collectionTCE_Map.put(key, t);
				    
			    }
			    else tce_Station.put(name+"@"+airplaneNumb, processingStation); //добавление ТСЕ с неизвестными ресурсами в map, чтобы потом при обработке последних ТСЕ не ввести их в модель
			   	last=false;
			   	processingStation="";
			    
			}
	     }
     }
//сортировка коллекции самолётов по названию, чтобы выдавать цвета по очереди
	  sortAirplanes(airplanes);
	  
	//выдача цветов
	for(String a:airplanes){
		if (colorInd>collectionColor.size()-1) colorInd=0;
	    if(!collectionAirplane_Color.containsKey(a)){
	    	collectionAirplane_Color.put(a,collectionColor.get(colorInd));
	    	colorInd++;
	    	}
	}
	//чтение связей для восстановления пропущенных связей тсе-родитель
	readConnections(wb, sheet2);
	
	//создание обратных связей между ТСЕ и обработка последних ТСЕ
	createLinksAndProcessLastTCE(tce_Station);
	
	
	Collections.sort(collectionTCE, new Comparator<TCE>() {
			public int compare(TCE t1, TCE t2) {
			return t1.dateStart.compareTo(t2.dateStart);
	  	}
	  });
		Calendar cal = Calendar.getInstance();
		cal.setTime(collectionTCE.get(0).dateStart);
		int year = cal.get(Calendar.YEAR);
		int month = cal.get(Calendar.MONTH);
		int day = cal.get(Calendar.DAY_OF_MONTH);
		getEngine().setStartDate(toDate(year, month, day-2, 0, 0, 0));
		
		TCE det=new TCE(date(),0,ПРОСК,null,null,detailsName,"",null);
		det.entered=false;
		det.ready=false;
		collectionTCE.addFirst(det);
		collectionTCE_Map.put(detailsName+"@МС.0000",det);
		
	for(Station s:collectionStationWithAlternates)
		s.dateEndExamplar=date();
	for (TCE t : collectionTCE) {
			if(t.processingStation==null)
				t.processingStation=getFreeStation(t.stationType,t.airplane,t);
			if(t.rack!=null) t.rack.collectionAirplane_LastTCE.put(t.airplane,t);
			if(t.processingStation.nonBreaking && t.nextTCE!=null)
				//определяем последнее обрабатываемое ТСЕ на станции для каждого самолёта. Коллекция уже отсортирована, поэтому не надо проверять даты
				t.processingStation.collectionAirplane_LastTCE.put(t.airplane, t); 
	      if(t.nextTCE!=null){
	       if(t.processingStation!=null)
	        	fw.append(t.name+" "+t.dateStart+" "+t.duration+" "+t.dateEnd+" "+t.processingStation.name+" "+t.nextTCEString+" "+t.nextTCE.name+" "+t.airplane+" "+((t.rack!=null)? t.rack.name:"null"));
	        else
	        	fw.append(t.name+" "+t.dateStart+" "+t.duration+" "+t.dateEnd+" "+t.stationType+" "+t.nextTCEString+" "+t.nextTCE.name+" "+t.airplane+" "+((t.rack!=null)? t.rack.name:"null"));
	        }
	      else  {
	      	if(t.processingStation!=null)
	      			fw.append(t.name+" "+t.dateStart+" "+t.duration+" "+t.dateEnd+" "+t.processingStation.name+" "+t.nextTCEString+" "+t.airplane+" "+((t.rack!=null)? t.rack.name:"null"));
	      	else
	        	fw.append(t.name+" "+t.dateStart+" "+t.duration+" "+t.dateEnd+" "+t.stationType+" "+t.nextTCEString+" "+t.nextTCE.name+" "+t.airplane+" "+((t.rack!=null)? t.rack.name:"null"));
	      	}
	      fw.append('\n');
	      if(t.prevTCE!=null)
		     for(TCE prev:t.prevTCE)
		     {
		       fw.append(prev.name+"\n");
		        if(t.dateStart.before(prev.dateStart))
		        	{
		        	fw.append("__________________________________________________\n"+
					"ВНИМАНИЕ. ТСЕ-ПРЕДОК "+" ПОСТУПАЕТ В МОДЕЛЬ ПОЗЖЕ СВОЕГО НАСЛЕДНИКА. "+ prev.dateStart+
					 "\n__________________________________________________\n");
		        	}
		        }
		}
	fw.close();
}
catch(IOException ex){}  

Метод чтения расписания станций readSchedule

		XSSFRow row;
		XSSFCell cell;
	   	Iterator rows = sheet.rowIterator();
	   	DataFormatter formatter=new DataFormatter();
   		FormulaEvaluator evaluator=new XSSFFormulaEvaluator(wb);
   		boolean first=true;
	    String nameStation;
	    String dateStartStr;
		String dateEndStr;
		String durationStr;
	 	Date dateStart;
		Date dateEnd; 
		int value;
		int[] values;
		row = (XSSFRow) rows.next();
	    while (rows.hasNext()) {
	    	values=new int[7];
	        row = (XSSFRow) rows.next();
	        Iterator cells = row.cellIterator();
	        cell = (XSSFCell) cells.next();
	        evaluator.evaluate(cell);
        	nameStation=formatter.formatCellValue(cell, evaluator);
	        nameStation=nameStation.trim(); 
	        nameStation=nameStation.replaceAll("[\\s]{2,}", " ");//удаление повторяющихся пробелов внутри строки
	        nameStation = russianToEnglish(nameStation);
	         nameStation=getNewNameStation(nameStation);    
	        
	        if(!nameStation.equals("")){
	        
		        cell = (XSSFCell) cells.next();
		        evaluator.evaluate(cell);
	       	 	dateStartStr=formatter.formatCellValue(cell, evaluator);
	       		dateStartStr=dateStartStr.trim();
		        
		        cell = (XSSFCell) cells.next();
		        evaluator.evaluate(cell);
	       	 	dateEndStr=formatter.formatCellValue(cell, evaluator);
	       		dateEndStr=dateEndStr.trim();
		        for(int i=0;i<7;i++)
		        {
		        	if(cells.hasNext()){
			        	cell = (XSSFCell) cells.next();
			        	evaluator.evaluate(cell);
			        	String str_value=formatter.formatCellValue(cell, evaluator);
			        	str_value=str_value.replace(',','.');
			       		values[i] = (int)Double.parseDouble(str_value);
		       		}
		       		else values[i]=0;
		        }
		        
				dateStart = getCalendarFromStr(dateStartStr).getTime();
				
				dateEnd = getCalendarFromStr(dateEndStr).getTime();
				
				if(first){
					lastDateSchedule=dateEnd; 
					first=false;
				}
				else
					if(lastDateSchedule.before(dateEnd)) lastDateSchedule=dateEnd;
				StationSchedule sched=new StationSchedule(dateStart,dateEnd,values);
				if(collectionStation_Map.containsKey(nameStation)){
					collectionStation_Map.get(nameStation).s_schedules.add(sched);
				}
				else if(collectionType_Station.containsKey(nameStation))
				{
					for(Station s:collectionType_Station.get(nameStation))
						s.s_schedules.add(sched);
				}
			}
	     }

Метод readConnections

		XSSFRow row;
		XSSFCell cell;
	   	Iterator rows = sheet.rowIterator();
	   	DataFormatter formatter=new DataFormatter();
   		FormulaEvaluator evaluator=new XSSFFormulaEvaluator(wb);
	    String predecessorTCE;
	    
		LinkedHashMap <String,LinkedList<String>> map=new LinkedHashMap <String,LinkedList<String>>();
		double delay;
		row = (XSSFRow) rows.next();
	    while (rows.hasNext()) {
		    String TCE;
			String airplane;
	        row = (XSSFRow) rows.next();
	        Iterator cells = row.cellIterator();
	        cell = (XSSFCell) cells.next();
	        evaluator.evaluate(cell);
        	predecessorTCE=formatter.formatCellValue(cell, evaluator);
	        predecessorTCE=predecessorTCE.trim(); 
	        
	        cell = (XSSFCell) cells.next();
	        evaluator.evaluate(cell);
       	 	airplane=formatter.formatCellValue(cell, evaluator);
       		airplane=airplane.trim();
	        
	        cell = (XSSFCell) cells.next();
	        evaluator.evaluate(cell);
       	 	TCE=formatter.formatCellValue(cell, evaluator);
       		TCE=TCE.trim();
       		if(cells.hasNext()) {
	       		cell = (XSSFCell) cells.next();
		        evaluator.evaluate(cell);
	       	 	String delayString=formatter.formatCellValue(cell, evaluator);
	       		delay=Double.parseDouble(delayString.replace(',','.'));
       		}
       		else delay=0;
       		String key1=TCE+"@"+airplane; //ТСЕ
       		String key2=predecessorTCE+"@"+airplane; //предшествующая ТСЕ 
       		
       		if(!map.containsKey(key2)) map.put(key2,new LinkedList<String>());
       		if(!TCE.equals(predecessorTCE)) //проверка, чтобы тсе не была сама себе предшественником
       			map.get(key2).add(key1);
       	}
	   createTCEfromLinks(map);
  

Метод обработки произведённой ТСЕ и добавление её в очередь queue следующей станции nextTCE:

TCE next=agent.nextTCE;
main.readyTCE++;
next.factDateEnd=date();
next.processingStation.processingTime=0;
next.processingStation.currentProcessingTime=0;
	try{
		FileWriter fw=new FileWriter("dates.txt",true);
		fw.append(next.name+" "+next.airplane+" "+next.processingStation.name+"Длительность: "+next.duration+" Дата начала по плану: "+next.dateStart+" Фактическая дата начала: "+next.factDateStart+
		" Дата окончания по плану: "+next.dateEnd+" Фактическая дата окончания: "+next.factDateEnd);
		if(!next.factDateEnd.after(new Date(next.dateEnd.getTime()+1000*60*10))) //даём 10 минут запаса, чтобы отнести в "Вовремя"
			{
			main.madeInTimeTCE++;
			fw.append(" Вовремя\n");
			}
		else
			fw.append(" Не вовремя\n");
		
		fw.close();
	}
	catch(IOException ex){}
	main.madeTCE++;
next.ready=true;
next.processingStation.textProcessingPersent.setVisible(false);
if(alternates||alternate){
	main.presentation.remove(next.processingStation.textProcessingPersent);
	main.presentation.remove(next.processingStation.textNameStation);
}
PalletRackLocation c=stock.getFreeCell(true);
if(next.nextTCE!=null){
	if(next.nextTCE.processingStation==null)
		next.nextTCE.processingStation=main.getFreeStation(next.nextTCE.stationType, next.nextTCE.airplane,next.nextTCE);
	//добавление ТСЕ в очередь следующей станции
	if(!next.nextTCE.processingStation.queue.contains(next.nextTCE)) //проверка на всякий случай
	{
		traceln(next.name+" "+next.nextTCE.name);
		next.nextTCE.processingStation.queue.add(next.nextTCE);
		//если заполнили пустой queue - возобновляем событие для проверки коллекции
		if(next.nextTCE.processingStation.queue.size()==1)next.nextTCE.processingStation.eventCheckQueue.restart(0);
		Collections.sort(next.nextTCE.processingStation.queue, new Comparator<TCE>() {
		public int compare(TCE t1, TCE t2) {
		if(t1.nextTCE != null && t2.nextTCE != null && t1.nextTCE.dateStart!=null && t2.nextTCE.dateStart!=null )
			return t1.nextTCE.dateStart.compareTo(t2.nextTCE.dateStart);
		return 0;
		}});
	}
	
		
	if(c!=null)
	{
		if(parallelProcessing&&stockForParallelProcessing!=null){
			stockForParallelProcessing.remove(next);
			countInParallelStock--;
			processingTCEs.remove(next);
		}
		if((alternates||alternate)&&stockForAlternate!=null){
			stockForAlternate.remove(next);
			main.presentation.remove(next.processingStation.textProcessingPersent);
			main.presentation.remove(next.processingStation.textNameStation);
		}
		
		next.setNetwork(network); //нужно поменять сеть ТСЕ на сеть внутри станции, чтобы отображалось в стеллаже
		next.group.setVisible(stock.isVisible());
		stock.put(c.row, c.position, c.level, true, next);
		readyTCEInStock.add(next);
		next.datePutInStock=date();
		double rotation;
		if(stock.getRotation()>4.71) rotation=0;
		else rotation=stock.getRotation()-1.57;
		next.setRotation(rotation);	
		
		if(processingTCEs.size()>0) 
			occupantTCE=processingTCEs.entrySet().iterator().next().getKey(); //получаем ключ первой записи
		else{
			next.processingStation.occupantTCE=null;
			next.processingStation.isBusy=false;
		}
		//проверка, надо ли ещё занимать станцию этим самолётом или обработанная ТСЕ была последенй
		if(next==next.processingStation.collectionAirplane_LastTCE.get(next.airplane))
			next.processingStation.occupantAirplane=null;
		
		//освобождение рамы
		if(next.rack!=null){
			delayRack.stopDelay(next.rack);
		}
	}
}
else 
{
		if(parallelProcessing&&stockForParallelProcessing!=null){
			stockForParallelProcessing.remove(next);
			countInParallelStock--;
			processingTCEs.remove(next);
		}
		if((alternates||alternate)&&stockForAlternate!=null){
			stockForAlternate.remove(next);
			main.presentation.remove(next.processingStation.textProcessingPersent);
			main.presentation.remove(next.processingStation.textNameStation);
		}
		
		if(processingTCEs.size()>0) 
			occupantTCE=processingTCEs.entrySet().iterator().next().getKey(); //получаем ключ первой записи
		else{
			next.processingStation.occupantTCE=null;
			next.processingStation.isBusy=false;
		}
		
		//проверка, надо ли ещё занимать станцию этим самолётом или обработанная ТСЕ была последенй
		if(next==next.processingStation.collectionAirplane_LastTCE.get(next.airplane))
			next.processingStation.occupantAirplane=null;
		//освобождение рамы
		if(next.rack!=null){
			delayRack.stopDelay(next.rack);
		}
		main.enterToStockFinal.take(next);
}
if(next.nextTCE!=null)
	checkReadinessNext(next.nextTCE,mainn);
for(TCE t:readyTCEInStockForDelete)
	t.processingStation.queue.remove(t);
readyTCEInStockForDelete.clear();
for(int i=0;i<queue.size();i++)
{
	if(!queue.get(i).processingStation.isBusy || parallelProcessing){
		checkReadinessNext(queue.get(i),mainn);
		for(TCE t:readyTCEInStockForDelete)
			t.processingStation.queue.remove(t);
		readyTCEInStockForDelete.clear();
	}
}

Метод проверки возможности начала производства и ввода комплектующих на станцию checkReadinessNext

boolean produced=true;
boolean freed=false;
boolean parallel=false;
TCE next=tce;
//отправление next на следующую станцию
if(next!=null)
{
	if(next.processingStation==null){
		next.processingStation=main.getFreeStation(next.stationType,next.airplane,next);
	}
	
	long timeReserve=mainn.minutesReserve*60*1000; //minutesReserve в милисекундах, чтобы подавать ТСЕ на станцию немного заранее
	if(next.processingStation.isBusy && next.processingStation.parallelProcessing && 
	next.processingStation.occupantTCE.airplane.equals(next.airplane)){
		if(next.processingStation.processingTCEs.size()!=0){ //если на || станцию одновр поступают несколько ТСЕ processingTCEs будет пустой
			for(TCE t:next.processingStation.processingTCEs.keySet())
				if(!next.dateStart.before(new Date(t.dateStart.getTime()-timeReserve)) && !next.dateStart.after(t.dateEnd) 			
				){
					 parallel=true;
				 }
		}
		else parallel=true;
	}
	//проверка, что все предшественники произведены
	boolean predecessorsReady=true;
	for(TCE t:next.predecessorsTCE)
		if(!t.ready) predecessorsReady=false;
		
	if(date().after(new Date(next.dateStart.getTime()-timeReserve)) && predecessorsReady && 
	//обеспечение неразрывности операций на F10,F20,...
	(!next.processingStation.nonBreaking || next.processingStation.occupantAirplane==null || next.processingStation.occupantAirplane.equals(next.airplane)) &&
	//проверка доступности рамы. Либо она не нужна, либо она не занята никаким самолётом, либо она свободна и занята тем же самолётом, что и next
	(next.rack==null || next.rack.occupantAirplane==null || (next.rack.occupantAirplane.equals(next.airplane) && !next.rack.isBusy))&& 
	(!next.processingStation.isBusy||next.processingStation.occupantTCE==next || 
	(parallel && next.processingStation.occupantTCE.airplane.equals(next.airplane))))
	{	
		if(next.processingStation.nonBreaking) next.processingStation.occupantAirplane=next.airplane;
		//проверка, все ли необходимые ТСЕ были произведены
		if(next.prevTCE!=null)
			for(TCE t:next.prevTCE)
				if(!t.ready) produced=false;
		if(produced)
		{	
			readyTCEInStockForDelete.add(next);
			if(next.prevTCE!=null)
				for(TCE t:next.prevTCE)	
				{
					//удаление ТСЕ со станций alternate, которые не могли войти в Stock
					if((t.processingStation.alternates ||t.processingStation.alternate) && t.processingStation.stockForAlternate.contains(t))
						{
							t.processingStation.stockForAlternate.remove(t);
							main.presentation.remove(t.processingStation.textProcessingPersent);
							main.presentation.remove(t.processingStation.textNameStation);							
						}
						//если ТСЕ не смог уйти в буфер и занимает станцию
						if(t==t.processingStation.occupantTCE){
							t.processingStation.isBusy=false;
							t.processingStation.occupantTCE=null;
						}
				}
			next.processingStation.isBusy=true;
			//ввод рамы
			if(next.rack!=null){
                 mainn.enterRack.out.connect(next.processingStation.rackIn);
				 mainn.palletRackForRack.remove(next.rack);
				 LinkedHashMap<String,TCE> air_last= next.rack.collectionAirplane_LastTCE;
				 main.enterRack.take(next.rack); //после этого поля почему-то сбрасываются рамы :\
				 next.rack.isBusy=true;
				 next.rack.occupantTCE=next;
				 next.rack.occupantAirplane=next.airplane;
				 next.rack.collectionAirplane_LastTCE=air_last;
			 }
			
			if(next.processingStation.occupantTCE==null)
				next.processingStation.occupantTCE=next; 
			TCE newDet;
			if(next.prevTCE!=null)
				//ввод комплектующих для ТСЕ
				for(TCE t:next.prevTCE)	
					{						
						t.processingStation.stock.remove(t);
						t.processingStation.readyTCEInStock.remove(t);
						t.group.setVisible(true);
						//перенос ТСЕ со станции в хранилище
						for(Station s:t.processingStation.collectionAlternates)
							if(s.isBusy && s.occupantTCE==null){
							TCE ot=s.occupantTCE;
							PalletRackLocation c=s.stock.getFreeCell(true);
							if((alternates||alternate)&&stockForAlternate!=null){
								stockForAlternate.remove(ot);
								main.presentation.remove(s.textProcessingPersent);
								main.presentation.remove(s.textNameStation);
							}
							s.stock.put(c.row, c.position, c.level, true, ot);
							readyTCEInStock.add(ot);
							ot.datePutInStock=date();
							double rotation;
							if(s.stock.getRotation()>4.71) rotation=0;
							else rotation=s.stock.getRotation()-1.57;
							ot.setRotation(rotation);	
						}
						if(t.processingStation.isBusy && t.processingStation.occupantTCE==null){
							TCE ot=t.processingStation.occupantTCE;
							PalletRackLocation c=t.processingStation.stock.getFreeCell(true);
							if((alternates||alternate)&&stockForAlternate!=null){
								stockForAlternate.remove(ot);
								main.presentation.remove(t.processingStation.textProcessingPersent);
								main.presentation.remove(t.processingStation.textNameStation);
							}
							t.processingStation.stock.put(c.row, c.position, c.level, true, ot);
							readyTCEInStock.add(ot);
							ot.datePutInStock=date();
							double rotation;
							if(t.processingStation.stock.getRotation()>4.71) rotation=0;
							else rotation=t.processingStation.stock.getRotation()-1.57;
							ot.setRotation(rotation);	
						}
						t.dateGetFromStock=date();
						if(t.datePutInStock!=null){
							long diff=t.dateGetFromStock.getTime()-t.datePutInStock.getTime();
							int days=(int)(diff/ (24 * 60 * 60 * 1000));
							main.allTimeInBuf+=days;
							if(days>main.maxTimeInBuf) main.maxTimeInBuf=days;
							if(days<main.minTimeInBuf) main.minTimeInBuf=days;
						}
						
						//ввод конкретной комплектующей для ТСЕ
						t.entered=true;
						main.enterTCE.take(t);
						if(t.processingStation.occupantTCE!=null && t.processingStation.occupantTCE.ready) 
						{	
							PalletRackLocation c=t.processingStation.stock.getFreeCell(true);
							if(c!=null){
								t.processingStation.stock.put(c.row, c.position, c.level, true, t.processingStation.occupantTCE);
								t.processingStation.readyTCEInStock.add(t.processingStation.occupantTCE);
								t.datePutInStock=date();
								double rotation;
								if(t.processingStation.stock.getRotation()>4.71) rotation=0;
								else rotation=t.processingStation.stock.getRotation()-1.57;
								t.processingStation.occupantTCE.setRotation(rotation);	
							}
						}
					}
			//ввод ДСЕ
				newDet=new TCE(null,0,null,next,null,main.detailsName,next.airplane,null);
				newDet.rectangle.setFillColor(tce.rectangle.getFillColor());
				main.enterDetails.take(newDet);

		}
	}
}
if(freed) {
	for(TCE t:queue)
		if(!isBusy)
			checkReadinessNext(t, main);
	for(TCE t:readyTCEInStockForDelete)
		t.nextTCE.processingStation.queue.remove(t);
	readyTCEInStockForDelete.clear();
}
