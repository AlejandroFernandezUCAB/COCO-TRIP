import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { NuevosIntegrantesPage } from './nuevos-integrantes';

@NgModule({
  declarations: [
    NuevosIntegrantesPage,
  ],
  imports: [
    IonicPageModule.forChild(NuevosIntegrantesPage),
  ],
})
export class NuevosIntegrantesPageModule {}
