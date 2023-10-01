import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgChartsModule } from 'ng2-charts';
import { ResultsRoutingModule } from './results-routing.module';
import { ResultsComponent } from './results.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ResultsChartComponent } from './components/results-chart/results-chart.component';


@NgModule({
  declarations: [
    ResultsComponent,
    ResultsChartComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ResultsRoutingModule,
    NgChartsModule
  ]
})
export class ResultsModule { }
