import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeacherComponent } from '../../teacher.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';
import { MaterialModule } from 'src/app/helpers/material.module';
import { MainModule } from 'src/app/lib/main/main.module';
import { ApplicationPipesModule } from 'src/app/pipes/application-pipes.module';
import { AddInformationBlockComponent } from './dialogs/add-information-block/add-information-block.component';
import { EditCompetenceFormationLevelComponent } from './dialogs/edit-competence-formation-level/edit-competence-formation-level.component';
import { EditLectureDialogComponent } from './dialogs/edit-lecture-dialog/edit-lecture-dialog.component';
import { EditLiteratureDialogComponent } from './dialogs/edit-literature-dialog/edit-literature-dialog.component';
import { EditMethodicalRecommendationComponent } from './dialogs/edit-methodical-recommendation/edit-methodical-recommendation.component';
import { EditTrainingCourseFormDialogComponent } from './dialogs/edit-training-course-form-dialog/edit-training-course-form-dialog.component';
import { EditWeekDialogComponent } from './dialogs/edit-week-dialog/edit-week-dialog.component';
import { AudienceComponent } from './components/audience/audience.component';
import { CompetenceFormationLevelsComponent } from './components/competence-formation-levels/competence-formation-levels.component';
import { EvaluationToolsComponent } from './components/evaluation-tools/evaluation-tools.component';
import { InformationBlockComponent } from './components/information-block/information-block.component';
import { LaboratoryLessonsComponent } from './components/laboratory-lessons/laboratory-lessons.component';
import { LecturesComponent } from './components/lectures/lectures.component';
import { LiteraturesComponent } from './components/literatures/literatures.component';
import { MethodicalRecommendationsComponent } from './components/methodical-recommendations/methodical-recommendations.component';
import { PracticalLessonsComponent } from './components/practical-lessons/practical-lessons.component';
import { ReviewerComponent } from './components/reviewer/reviewer.component';
import { SemestersComponent } from './components/semesters/semesters.component';
import { TrainingCourseFormComponent } from './components/training-course-form/training-course-form.component';
import { WeeksComponent } from './components/weeks/weeks.component';
import { EditEducationalProgramComponent } from './edit-educational-program.component';
import { RouterModule } from '@angular/router';
import { CompetenceFormationLevelTableComponent } from './components/competence-formation-level-table/competence-formation-level-table.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { DirtyCheckGuard } from 'src/app/helpers/guards/dirty-check-guard';
import { SaveInformationBlockComponent } from './dialogs/save-information-block/save-information-block.component';
import { NgScrollbarModule } from 'ngx-scrollbar';
@NgModule({
  declarations: [
    EditEducationalProgramComponent,
    WeeksComponent,
    PracticalLessonsComponent,
    LaboratoryLessonsComponent,
    SemestersComponent,
    EditWeekDialogComponent,
    TrainingCourseFormComponent,
    EditTrainingCourseFormDialogComponent,
    EvaluationToolsComponent,
    CompetenceFormationLevelsComponent,
    EditCompetenceFormationLevelComponent,
    InformationBlockComponent,
    AddInformationBlockComponent,
    LiteraturesComponent,
    EditLiteratureDialogComponent,
    EditMethodicalRecommendationComponent,
    MethodicalRecommendationsComponent,
    LecturesComponent,
    EditLectureDialogComponent,
    AudienceComponent,

    ReviewerComponent,
    CompetenceFormationLevelTableComponent,
    SaveInformationBlockComponent,
  ],
  imports: [
    CommonModule,
    MainModule,
    ReactiveFormsModule,
    NgbModule,
    FontAwesomeModule,
    MaterialModule,
    FormsModule,
    NgScrollbarModule,
    CommonModule,
    NgxSpinnerModule,
    ApplicationPipesModule,
    CKEditorModule,
    RouterModule.forChild([
      {
        path: '',
        component: EditEducationalProgramComponent,
        children: [
          {
            path: 'information-block/:number',
            component: InformationBlockComponent,
            canDeactivate: [DirtyCheckGuard],
          },
          {
            path: 'lectures',
            component: LecturesComponent,
          },
          {
            path: 'practicalLessons',
            component: PracticalLessonsComponent,
          },
          {
            path: 'laboratorylLessons',
            component: LaboratoryLessonsComponent,
          },
          {
            path: 'semesters',
            component: SemestersComponent,
          },
          {
            path: 'trainingCourseForm',
            component: TrainingCourseFormComponent,
          },
          {
            path: 'evaluationTools',
            component: EvaluationToolsComponent,
          },
          {
            path: 'competenceFormationLevels',
            component: CompetenceFormationLevelsComponent,
          },
          {
            path: 'methodicalRecommendations',
            component: MethodicalRecommendationsComponent,
          },
          {
            path: 'audience',
            component: AudienceComponent,
          },
          {
            path: 'reviewer',
            component: ReviewerComponent,
          },
          {
            path: 'literature/:type',
            component: LiteraturesComponent,
          },
          {
            path: '',
            redirectTo: 'information-block/1.1',
            pathMatch: 'full',
          },
        ],
      },
    ]),
  ],
})
export class EditEducationalProgramModule {}
