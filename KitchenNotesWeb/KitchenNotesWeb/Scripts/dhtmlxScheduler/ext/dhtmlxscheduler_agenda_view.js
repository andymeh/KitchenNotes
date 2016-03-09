/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(a){a.date.add_agenda=function(d){return a.date.add(d,1,"year")};a.templates.agenda_time=function(d,e,c){return c._timed?this.day_date(c.start_date,c.end_date,c)+" "+this.event_date(d):a.templates.day_date(d)+" &ndash; "+a.templates.day_date(e)};a.templates.agenda_text=function(a,e,c){return c.text};a.templates.agenda_date=function(){return""};a.date.agenda_start=function(){return a.date.date_part(new Date)};a.attachEvent("onTemplatesReady",function(){function d(c){if(c){var d=
a.locale.labels;a._els.dhx_cal_header[0].innerHTML="<div class='dhx_agenda_line'><div>"+d.date+"</div><span style='padding-left:25px'>"+d.description+"</span></div>";a._table_view=!0;a.set_sizes()}}function e(){var c=a._date,d=a.get_visible_events();d.sort(function(a,b){return a.start_date>b.start_date?1:-1});for(var e="<div class='dhx_agenda_area'>",f=0;f<d.length;f++){var b=d[f],h=b.color?"background:"+b.color+";":"",i=b.textColor?"color:"+b.textColor+";":"",j=a.templates.event_class(b.start_date,
b.end_date,b);e+="<div class='dhx_agenda_line"+(j?" "+j:"")+"' event_id='"+b.id+"' style='"+i+""+h+""+(b._text_style||"")+"'><div class='dhx_agenda_event_time'>"+a.templates.agenda_time(b.start_date,b.end_date,b)+"</div>";e+="<div class='dhx_event_icon icon_details'>&nbsp</div>";e+="<span>"+a.templates.agenda_text(b.start_date,b.end_date,b)+"</span></div>"}e+="<div class='dhx_v_border'></div></div>";a._els.dhx_cal_data[0].innerHTML=e;a._els.dhx_cal_data[0].childNodes[0].scrollTop=a._agendaScrollTop||
0;var g=a._els.dhx_cal_data[0].childNodes[0],l=g.childNodes[g.childNodes.length-1];l.style.height=g.offsetHeight<a._els.dhx_cal_data[0].offsetHeight?"100%":g.offsetHeight+"px";var k=a._els.dhx_cal_data[0].firstChild.childNodes;a._els.dhx_cal_date[0].innerHTML=a.templates.agenda_date(a._min_date,a._max_date,a._mode);a._rendered=[];for(f=0;f<k.length-1;f++)a._rendered[f]=k[f]}var c=a.dblclick_dhx_cal_data;a.dblclick_dhx_cal_data=function(){if(this._mode=="agenda")!this.config.readonly&&this.config.dblclick_create&&
this.addEventNow();else if(c)return c.apply(this,arguments)};a.attachEvent("onSchedulerResize",function(){return this._mode=="agenda"?(this.agenda_view(!0),!1):!0});var h=a.render_data;a.render_data=function(a){if(this._mode=="agenda")e();else return h.apply(this,arguments)};var i=a.render_view_data;a.render_view_data=function(){if(this._mode=="agenda")a._agendaScrollTop=a._els.dhx_cal_data[0].childNodes[0].scrollTop,a._els.dhx_cal_data[0].childNodes[0].scrollTop=0;return i.apply(this,arguments)};
a.agenda_view=function(c){a._min_date=a.config.agenda_start||a.date.agenda_start(a._date);a._max_date=a.config.agenda_end||a.date.add_agenda(a._min_date,1);a._table_view=!0;d(c);c&&e()}})});
