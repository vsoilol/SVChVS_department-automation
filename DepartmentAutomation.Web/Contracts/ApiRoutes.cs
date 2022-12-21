namespace DepartmentAutomation.Web.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class Discipline
        {
            public const string Base = Root + "/discipline";

            public const string BaseWithId = Base + "/{id}";

            public const string GetBriefInfo = Base + "/brief/{id}";
            
            public const string GetWithPagination = Base + "/pagination";
            
            public const string GetAdditionalDisciplineInfoById = Base + "/additional/{disciplineId}";

            public const string UpdateDisciplineTeachers = Base + "/teachers";
            
            public const string ChangeStatus = Base + "/changeStatus";

            public const string IsEducationalProgramExist = Base + "/isProgramExist/{id}";
        }

        public static class Teacher
        {
            public const string Base = Root + "/teacher";

            public const string GetWithPagination = Base + "/pagination";

            public const string ChangeTeacherPassword = Base + "/changeTeacherPassword";

            public const string GetTeachersByDepartmentId = Base + "/byDepartment/{departmentId}";

            public const string GetTeachersFullNameByDisciplineId = Base + "/byDiscipline/{disciplineId}";
        }

        public static class EducationalProgram
        {
            public const string Base = Root + "/educationalProgram";

            public const string BaseWithId = Base + "/{id}";

            public const string GetAllByTeacherId = Base + "/byTeacher/{teacherId}";

            public const string GetWithPagination = Base + "/pagination";

            public const string CreateDefault = Base + "/default/{disciplineId}";

            public const string DownloadWordDocument = Base + "/wordDocument/{id}";

            public const string UpdateAudiencesInfo = Base + "/audiences";

            public const string ChangeStatus = Base + "/changeStatus";
        }

        public static class InformationBlock
        {
            public const string Base = Root + "/informationBlock";

            public const string GetMainBlocksByProgramId = Base + "/mainBlocks/{educationalProgramId}";

            public const string GetEditableBlocksByProgramId = Base + "/editableBlocks/{educationalProgramId}";

            public const string GetLastBlocksByProgramId = Base + "/lastBlocks/{educationalProgramId}";

            public const string GetNotChoosenBlocksByProgramId = Base + "/notChoosenBlocks/{educationalProgramId}";

            public const string GetTemplatesByInformationBlockId = Base + "/templates/{informationBlockId}";

            public const string GetBlockByNumber = Base + "/byNumber";

            public const string GetAdditionalBlocksNameByProgramId = Base + "/blocksName/{educationalProgramId}";
        }
        
        public static class Indicator
        {
            public const string Base = Root + "/indicator";
            
            public const string GetIndicatorWithLevelsByProgramId = Base + "/with-levels/{educationalProgramId}";
        }

        public static class EvaluationTool
        {
            public const string Base = Root + "/evaluationTool";

            public const string GetNotChoosenEvaluationTool = Base + "/notChoosenEvaluationTool/{educationalProgramId}";

            public const string GetAllEvaluationToolByProgramId = Base + "/byProgramId/{educationalProgramId}";

            public const string GetAllEvaluationToolTypeByProgramId = Base + "/type/{educationalProgramId}";
        }

        public static class CompetenceFormationLevel
        {
            public const string Base = Root + "/competenceFormationLevel";

            public const string GetCompetenceFormationLevelByCompetenceId = Base + "/byCompetence";
        }
        
        public static class Curriculum
        {
            public const string Base = Root + "/curriculum";

            public const string GetAllYearsByDepartmentId = Base + "/years/{departmentId}";
        }

        public static class Lesson
        {
            public const string Base = Root + "/lesson";

            public const string GetAllLessonsByProgramId = Base + "/byProgramId";
            
            public const string GetAllLescturesByProgramId = Base + "/lectures/{educationalProgramId}";

            public const string BaseWithId = Base + "/{id}";
            
            public const string GetLectureById = Base + "/lecture/{id}";

            public const string GetAllLessonsWithoutWeek = Base + "/withoutWeek";

            public const string GetLessonByWeekId = Base + "/byWeek";

            public const string GetAllLessonsWithoutTrainingCourseForm = Base + "/withoutTrainingCourseForm";
        }

        public static class KnowledgeControlForm
        {
            public const string Base = Root + "/knowledgeControlForm";

            public const string GetAllByWeekId = Base + "/byWeek/{weekId}";
        }

        public static class Competence
        {
            public const string Base = Root + "/competence";

            public const string GetByLessonId = Base + "/byLesson/{lessonId}";

            public const string GetAllByProgramId = Base + "/{educationalProgramId}";
        }

        public static class Position
        {
            public const string GetAll = Root + "/position";
        }

        public static class Semester
        {
            public const string Base = Root + "/semester";

            public const string GetAllSemestersByProgramId = Base + "/byProgramId/{educationalProgramId}";
        }

        public static class Reviewer
        {
            public const string Base = Root + "/reviewer";

            public const string GetReviewerByProgramId = Base + "/byProgramId/{educationalProgramId}";
        }

        public static class Audience
        {
            public const string Base = Root + "/audience";

            public const string GetAllAudiencesByProgramId = Base + "/byProgramId/{educationalProgramId}";
        }

        public static class Week
        {
            public const string Base = Root + "/week";

            public const string GetTrainingModuleNumbers = Base + "/getTrainingModuleNumbers/{educationalProgramId}";

            public const string GetWeeksByModuleNumber = Base + "/byModuleNumber";
        }

        public static class MethodicalRecommendation
        {
            public const string Base = Root + "/methodicalRecommendation";

            public const string GetMethodicalRecommendationByProgramId = Base + "/byProgram/{educationalProgramId}";

            public const string BaseWithId = Base + "/{id}";
        }

        public static class Literature
        {
            public const string Base = Root + "/literature";

            public const string GetLiteraturesByType = Base + "/byType";

            public const string BaseWithId = Base + "/{id}";
        }

        public static class Department
        {
            public const string GetAll = Root + "/department";
        }

        public static class TrainingCourseForm
        {
            public const string Base = Root + "/trainingCourseForm";

            public const string GetAllByProgramId = Base + "/byProgram/{educationalProgramId}";

            public const string GetAllWithoutLessons = Base + "/withoutLessons/{educationalProgramId}";

            public const string AddLessonsToTrainingCourseForm = Base + "/addLessons";

            public const string DeleteLessonsFromTrainingCourseForm = Base + "/deleteLessons";
        }

        public static class Identity
        {
            public const string Login = Root + "/identity/login";
            
            public const string LoginByFullName = Root + "/identity/login/fullName";

            public const string Register = Root + "/identity/register";

            public const string Refresh = Root + "/identity/refresh";

            public const string Logout = Root + "/identity/logout";

            public const string ChangePassword = Root + "/identity/changePassword";

            public const string ActivateUser = Root + "/identity/activate/{id}";
            
            public const string DeactivateUser = Root + "/identity/deactivate/{id}";
        }
    }
}