import { Component, Input, OnInit } from '@angular/core';
import { MatchResult } from '../../models/results';
import { ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-results-chart',
  templateUrl: './results-chart.component.html',
  styleUrls: ['./results-chart.component.scss']
})
export class ResultsChartComponent implements OnInit {
  @Input() results!: MatchResult[];

  public radarChartOptions: ChartConfiguration<'radar'>['options'] = {
    responsive: true,
    elements: {
      line: {
        borderColor: '#3cba9f',
      },
      point: {
        radius: 5,
        borderWidth: 3,
        borderColor: '#3cba9f',
        backgroundColor: '#fff',
      }
    },
  };

  public radarChartLabels!: string[];

  public radarChartData!: ChartConfiguration<'radar'>['data']['datasets'];

  constructor() {}

  ngOnInit(): void {
    this.radarChartLabels = this.results.map(result => result.name);
    const data = this.results.map(result => result.percentageMatch);
    this.radarChartData = [
      { data: data,
        label: 'Dopasowanie',
        borderColor: '#3cba9f',
        pointBorderColor: '#3cba9f',
        backgroundColor: 'rgba(255, 255, 255, 0.2)', // Kolor wypełnienia
        pointHoverBackgroundColor: '#fff', // Kolor punktów podczas najazdu myszką
        pointHoverBorderColor: 'rgba(255, 255, 255, 1)', // Kolor obramowania punktów podczas najazdu myszką
      },
    ];
  }
}
