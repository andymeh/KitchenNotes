/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(a){a.attachEvent("onTemplatesReady",function(){function f(a,b,c,i){for(var j=b.getElementsByTagName(a),g=c.getElementsByTagName(a),e=g.length-1;e>=0;e--)if(c=g[e],i){var d=document.createElement("SPAN");d.className="dhx_text_disabled";d.innerHTML=i(j[e]);c.parentNode.insertBefore(d,c);c.parentNode.removeChild(c)}else c.disabled=!0}var s=a.config.lightbox.sections,h=null,o=a.config.buttons_left.slice(),p=a.config.buttons_right.slice();a.attachEvent("onBeforeLightbox",function(f){if(this.config.readonly_form||
this.getEvent(f).readonly){this.config.readonly_active=!0;for(var b=0;b<this.config.lightbox.sections.length;b++)this.config.lightbox.sections[b].focus=!1}else this.config.readonly_active=!1,a.config.buttons_left=o.slice(),a.config.buttons_right=p.slice();var c=this.config.lightbox.sections;if(this.config.readonly_active){for(var i=!1,b=0;b<c.length;b++)if(c[b].type=="recurring"){h=c[b];this.config.readonly_active&&c.splice(b,1);break}!i&&!this.config.readonly_active&&h&&c.splice(c.length-2,0,h);
for(var j=["dhx_delete_btn","dhx_save_btn"],g=[a.config.buttons_left,a.config.buttons_right],b=0;b<j.length;b++)for(var e=j[b],d=0;d<g.length;d++){for(var l=g[d],m=-1,k=0;k<l.length;k++)if(l[k]==e){m=k;break}m!=-1&&l.splice(m,1)}}this.resetLightbox();return!0});var q=a._fill_lightbox;a._fill_lightbox=function(){var h=q.apply(this,arguments);if(this.config.readonly_active){var b=this.getLightbox(),c=this._lightbox_r=b.cloneNode(!0);c.id=a.uid();f("textarea",b,c,function(a){return a.value});f("input",
b,c,!1);f("select",b,c,function(a){return a.options[Math.max(a.selectedIndex||0,0)].text});b.parentNode.insertBefore(c,b);n.call(this,c);a._lightbox&&a._lightbox.parentNode.removeChild(a._lightbox);this._lightbox=c;this.setLightboxSize();this._lightbox=null;c.onclick=function(c){var b=c?c.target:event.srcElement;if(!b.className)b=b.previousSibling;if(b&&b.className)switch(b.className){case "dhx_cancel_btn":a.callEvent("onEventCancel",[a._lightbox_id]),a._edit_stop_event(a.getEvent(a._lightbox_id),
!1),a.hide_lightbox()}}}return h};var n=a.showCover;a.showCover=function(){this.config.readonly_active||n.apply(this,arguments)};var r=a.hide_lightbox;a.hide_lightbox=function(){if(this._lightbox_r)this._lightbox_r.parentNode.removeChild(this._lightbox_r),this._lightbox_r=null;return r.apply(this,arguments)}})});
