import { Component, OnInit } from '@angular/core';
import { ResultsService } from './services/results.service';
import { ActivatedRoute } from '@angular/router';
import { Result } from './models/results';

@Component({
  selector: 'app-results',
  styleUrls: ['./results.component.scss'],
  templateUrl: './results.component.html'
})
export class ResultsComponent implements OnInit {
  public result!: Result;
  public isLoading = true;
  public resultsCount = 0;
  public mostPopularDiscipline = '';
  
  constructor(private resultsService: ResultsService, private route:ActivatedRoute) {
  }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.resultsService.getResults(id)
      .subscribe(x => {
        this.result = x;
        this.isLoading = false;
      });
  }

}
