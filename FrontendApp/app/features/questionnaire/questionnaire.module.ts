import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QuestionnaireRoutingModule } from './questionnaire-routing.module';
import { QuestionnaireComponent } from './questionnaire.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { QuestionComponent } from './components/question/question.component';


@NgModule({
  declarations: [
    QuestionnaireComponent,
    QuestionComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    QuestionnaireRoutingModule
  ]
})
export class QuestionnaireModule { }
