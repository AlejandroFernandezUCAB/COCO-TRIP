import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { BuscarAmigoPage } from './buscar-amigo';

@NgModule({
  declarations: [
    BuscarAmigoPage,
  ],
  imports: [
    IonicPageModule.forChild(BuscarAmigoPage),
  ],
})
export class AgregarAmigoPageModule {}
