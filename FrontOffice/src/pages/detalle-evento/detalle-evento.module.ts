import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DetalleEventoPage } from './detalle-evento';

@NgModule({
  declarations: [
    DetalleEventoPage,
  ],
  imports: [
    IonicPageModule.forChild(DetalleEventoPage),
  ],
})
export class DetalleEventoPageModule {}
