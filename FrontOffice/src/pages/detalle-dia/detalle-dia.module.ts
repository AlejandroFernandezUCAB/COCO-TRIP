import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DetalleDiaPage } from './detalle-dia';

@NgModule({
  declarations: [
    DetalleDiaPage,
  ],
  imports: [
    IonicPageModule.forChild(DetalleDiaPage),
  ],
})
export class DetalleDiaPageModule {}
