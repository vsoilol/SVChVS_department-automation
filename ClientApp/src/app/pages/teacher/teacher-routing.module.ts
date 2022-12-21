import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { TeacherComponent } from './teacher.component';
import { EducationalProgramComponent } from './components/educational-program/educational-program.component';
import { AuthTeacherGuard } from 'src/app/helpers/auth.teacher.guard';
import { InformationBlockComponent } from './pages/edit-educational-program/components/information-block/information-block.component';
import { NewsResolver } from 'src/app/resolvers/news.resolver';
import {GuideComponent} from "./components/guide/guide.component";

const routes: Routes = [
  {
    path: '',
    component: TeacherComponent,
    children: [
      {
        path: 'educational-program',
        component: EducationalProgramComponent,
        //resolve: { message: NewsResolver },
      },
      {
        path: 'guide',
        component: GuideComponent,
        //resolve: { message: NewsResolver },
      },
      {
        path: 'edit-educational-program/:id',
        loadChildren: () =>
          import(
            '../teacher/pages/edit-educational-program/edit-educational-program.module'
          ).then((m) => m.EditEducationalProgramModule),
      },
      {
        path: '',
        redirectTo: 'educational-program',
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TeacherRoutingModule {}
