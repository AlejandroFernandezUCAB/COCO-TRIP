import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { BorrarCuentaPage } from './borrar-cuenta';

@NgModule({
  declarations: [
    BorrarCuentaPage,
  ],
  imports: [
    IonicPageModule.forChild(BorrarCuentaPage),
  ],
})
export class BorrarCuentaPageModule {}
