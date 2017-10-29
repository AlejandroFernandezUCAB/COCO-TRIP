import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ConsultarItemModalPage } from './consultar-item-modal';

@NgModule({
  declarations: [
    ConsultarItemModalPage,
  ],
  imports: [
    IonicPageModule.forChild(ConsultarItemModalPage),
  ],
})
export class ConsultarItemModalPageModule {}
