import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TranslateModule } from '@ngx-translate/core';
import { BorrarCuentaPage } from './borrar-cuenta';

@NgModule({
  declarations: [
    BorrarCuentaPage,
  ],
  imports: [
    IonicPageModule.forChild(BorrarCuentaPage),
    TranslateModule
  ],
})
export class BorrarCuentaPageModule {}
