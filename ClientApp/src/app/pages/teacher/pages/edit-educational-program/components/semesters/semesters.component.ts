import { Component, OnInit } from '@angular/core';
import { Semester } from 'src/app/models/Response/Semester';
import { EducationalProgramDataService } from 'src/app/services/educational-program-data.service';
import { SemesterService } from 'src/app/services/semester.service';
import { WeekService } from 'src/app/services/week.service';

@Component({
  selector: 'app-semesters',
  templateUrl: './semesters.component.html',
  styleUrls: ['./semesters.component.scss'],
})
export class SemestersComponent implements OnInit {
  public semesters: Semester[];
  public moduleNumbers: number[];

  constructor(
    private semesterService: SemesterService,
    private educationalProgramData: EducationalProgramDataService,
    private weekService: WeekService
  ) {}

  ngOnInit(): void {
    this.getAllSemesters();

    this.weekService
      .getTrainingModuleNumbers(this.educationalProgramData.id)
      .subscribe((_) => {
        this.moduleNumbers = _;
      });
  }

  getAllSemesters() {
    this.semesterService
      .getAllSemestersByProgramId(this.educationalProgramData.id)
      .subscribe((_) => {
        this.semesters = _;
      });
  }
}
