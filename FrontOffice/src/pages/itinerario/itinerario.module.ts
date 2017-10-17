import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ItinerarioPage } from './itinerario';

@NgModule({
  declarations: [
    ItinerarioPage,
  ],
  imports: [
    IonicPageModule.forChild(ItinerarioPage),
  ],
})
export class ItinerarioPageModule {}
