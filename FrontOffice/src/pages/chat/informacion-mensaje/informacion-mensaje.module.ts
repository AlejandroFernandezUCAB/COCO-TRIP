import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { InformacionMensajePage } from './informacion-mensaje';

@NgModule({
  declarations: [
    InformacionMensajePage,
  ],
  imports: [
    IonicPageModule.forChild(InformacionMensajePage),
  ],
})
export class InformacionMensajePageModule {}
