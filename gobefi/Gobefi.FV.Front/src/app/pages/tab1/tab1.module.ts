import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { Tab1Page } from './tab1.page';
import { ExploreContainerComponentModule } from '../../explore-container/explore-container.module';

import { Tab1PageRoutingModule } from './tab1-routing.module';
import { ComponentsModule } from '../../components/components.module';
import { ModalVehiculoComponent } from 'src/app/components/modal-vehiculo/modal-vehiculo.component';



@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ExploreContainerComponentModule,
    Tab1PageRoutingModule,
    ComponentsModule
  ],
  declarations: [Tab1Page,ModalVehiculoComponent],
  entryComponents:[ModalVehiculoComponent]
})
export class Tab1PageModule {}
