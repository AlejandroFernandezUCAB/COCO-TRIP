import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { AgregarAmigoPage } from './agregar-amigo';

@NgModule({
  declarations: [
    AgregarAmigoPage,
  ],
  imports: [
    IonicPageModule.forChild(AgregarAmigoPage),
  ],
})
export class AgregarAmigoPageModule {}
