import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { AmigosPage } from './amigos';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    AmigosPage,
  ],
  imports: [
    IonicPageModule.forChild(AmigosPage),
    TranslateModule
  ],
})
export class AmigosPageModule {}
