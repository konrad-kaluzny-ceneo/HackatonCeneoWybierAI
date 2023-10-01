import { Component } from '@angular/core';
import { QuestionnaireService } from './services/questionnaire.service';
import { Questionnaire } from './models/questionnaire';
import { trigger, transition, style, animate, group, query, animateChild } from '@angular/animations';
import { Router } from '@angular/router';

@Component({
  selector: 'app-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.scss'],
  animations: [
    trigger('slideIn', [
      transition(':leave', [
        style({ transform: 'translateX(0%)' }),
        animate('500ms ease', style({ transform: 'translateX(-100%)', opacity:0 }))
      ]),
      transition(':enter', [
        style({ transform: 'translateX(100%)' }),
        animate('500ms ease', style({ transform: 'translateX(0)' }))
      ])
    ])
  ]
})
export class QuestionnaireComponent {

  public questionnaire!: Questionnaire;
  public currentQuestionIndex: number = 0;
  public loading = true;

  constructor(
    private questionnaireService: QuestionnaireService,
    private router: Router
    ) {
    this.questionnaireService.getQuestionnaire().subscribe(data => {
      this.questionnaire = data;
      this.loading = false;
    });
  }

  nextQuestion(index: number, value: boolean) {
    this.questionnaire.questions[index].answer = value;
    if (index === this.questionnaire.questions.length - 1) {
      this.loading = true;
      this.questionnaireService.sendFilledQuestionnaire(this.questionnaire)
      .subscribe((x) => {
          this.loading = false;
          this.router.navigateByUrl(`results/${x}`);
        });
    } else {
      this.currentQuestionIndex++;
    }
  }

  previousQuestion() {
    this.currentQuestionIndex--;
  }
}
