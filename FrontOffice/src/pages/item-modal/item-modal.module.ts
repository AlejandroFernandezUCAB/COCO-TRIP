import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ItemModalPage } from './item-modal';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    ItemModalPage,
  ],
  imports: [
    IonicPageModule.forChild(ItemModalPage),
    TranslateModule,
  ],
})
export class ItemModalPageModule {}
