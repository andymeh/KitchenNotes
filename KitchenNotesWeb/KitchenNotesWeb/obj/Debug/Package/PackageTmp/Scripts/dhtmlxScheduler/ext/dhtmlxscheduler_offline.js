/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(d){d.load=function(a,c,b){if(typeof c=="string")var h=this._process=c,c=b;this._load_url=a;this._after_call=c;a.$proxy?a.load(this,typeof h=="string"?h:null):this._load(a,this._date)};d._dp_init_backup=d._dp_init;d._dp_init=function(a){a._sendData=function(c,b){if(c){if(!this.callEvent("onBeforeDataSending",b?[b,this.getState(b),c]:[null,null,c]))return!1;b&&(this._in_progress[b]=(new Date).valueOf());if(this.serverProcessor.$proxy){var a=this._tMode!="POST"?"get":"post",
e=[],f;for(f in c)e.push({id:f,data:c[f],operation:this.getState(f)});this.serverProcessor._send(e,a,this)}else{var d=new dtmlXMLLoaderObject(this.afterUpdate,this,!0),g=this.serverProcessor+(this._user?getUrlSymbol(this.serverProcessor)+["dhx_user="+this._user,"dhx_version="+this.obj.getUserData(0,"version")].join("&"):"");this._tMode!="POST"?d.loadXML(g+(g.indexOf("?")!=-1?"&":"?")+this.serialize(c,b)):d.loadXML(g,!0,this.serialize(c,b));this._waitMode++}}};a._updatesToParams=function(c){for(var b=
{},a=0;a<c.length;a++)b[c[a].id]=c[a].data;return this.serialize(b)};a._processResult=function(a,b,d){if(d.status!=200)for(var e in this._in_progress){var f=this.getState(e);this.afterUpdateCallback(e,e,f,null)}else b=new dtmlXMLLoaderObject(function(){},this,!0),b.loadXMLString(a),b.xmlDoc=d,this.afterUpdate(this,null,null,null,b)};this._dp_init_backup(a)};if(window.dataProcessor)dataProcessor.prototype.init=function(a){this.init_original(a);a._dataprocessor=this;this.setTransactionMode("POST",!0);
this.serverProcessor.$proxy||(this.serverProcessor+=(this.serverProcessor.indexOf("?")!=-1?"&":"?")+"editing=true")}});
