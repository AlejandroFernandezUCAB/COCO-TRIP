import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ConfigNotificacionesItiPage } from './config-notificaciones-iti';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    ConfigNotificacionesItiPage,
  ],
  imports: [
    IonicPageModule.forChild(ConfigNotificacionesItiPage),
    TranslateModule,
  ],
})
export class ConfigNotificacionesItiPageModule {}
