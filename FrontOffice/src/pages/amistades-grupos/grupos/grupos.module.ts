import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { GruposPage } from './grupos';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    GruposPage,
  ],
  imports: [
    IonicPageModule.forChild(GruposPage),
    TranslateModule
  ],
})
export class GruposPageModule {}