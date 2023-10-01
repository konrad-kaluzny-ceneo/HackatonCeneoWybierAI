import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Question } from '../../models/questionnaire';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss']
})
export class QuestionComponent {
  @Input() question!: Question;
  @Output() answered = new EventEmitter<boolean>();

  public answerClick(answer: boolean): void {
    this.answered.emit(answer);
  }
}
