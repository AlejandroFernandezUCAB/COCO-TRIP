import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { EventosActividadesPage } from './eventos-actividades';

@NgModule({
  declarations: [
    EventosActividadesPage,
  ],
  imports: [
    IonicPageModule.forChild(EventosActividadesPage),
  ],
})
export class EventosActividadesPageModule {}
