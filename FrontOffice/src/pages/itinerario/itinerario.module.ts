import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ItinerarioPage } from './itinerario';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [
    ItinerarioPage,
  ],
  imports: [
    IonicPageModule.forChild(ItinerarioPage),
    TranslateModule,
  ],
})
export class ItinerarioPageModule {}
