import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TranslateModule } from '@ngx-translate/core';
import { PreferenciasPage } from './preferencias';

@NgModule({
  declarations: [
    PreferenciasPage,
  ],
  imports: [
    IonicPageModule.forChild(PreferenciasPage),
    TranslateModule
  ],
})
export class PreferenciasPageModule {}
