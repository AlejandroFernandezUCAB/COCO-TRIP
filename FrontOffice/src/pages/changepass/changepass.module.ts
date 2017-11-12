import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ChangepassPage } from './changepass';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    ChangepassPage,
  ],
  imports: [
    IonicPageModule.forChild(ChangepassPage),
    TranslateModule
  ],
})
export class ChangepassPageModule {}
