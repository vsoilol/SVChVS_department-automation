const PROXY_CONFIG = [
  {
    context: [
      "/api/identity/register",
      "/api/identity/login",
      "/api/identity/refresh",
      "/api/identity/logout",
      "/api/teacher",
      "/api/teacher/pagination",
      "/api/position",
      "/api/department",
      "/api/discipline/brief",
      "/api/teacher/changeTeacherPassword",
      "/api/identity/changePassword",
      "/api/educationalProgram/byTeacher",
      "/api/educationalProgram",
      "/api/educationalProgram/wordDocument",
      "/api/educationalProgram/audiences",
      "/api/informationBlock/mainBlocks",
      "/api/informationBlock",
      "/api/informationBlock/editableBlocks",
      "/api/informationBlock/lastBlocks",
      "/api/informationBlock/notChoosenBlocks",
      "/api/informationBlock/templates",
      "/api/lesson",
      "/api/lesson/byProgramId",
      "/api/competence",
      "/api/competence/byLesson",
      "/api/lesson/withoutWeek",
      "/api/semester/byProgramId",
      "/api/week/getTrainingModuleNumbers",
      "/api/week/byModuleNumber",
      "/api/knowledgeControlForm/byWeek",
      "/api/knowledgeControlForm",
      "/api/week",
      "/api/lesson/byWeek",
      "/api/lesson/withoutTrainingCourseForm",
      "/api/trainingCourseForm/byProgram",
      "/api/trainingCourseForm/withoutLessons",
      "/api/trainingCourseForm/addLessons",
      "/api/trainingCourseForm/deleteLessons",
      "/api/evaluationTool/notChoosenEvaluationTool",
      "/api/evaluationTool/byProgramId",
      "/api/evaluationTool",
      "/api/competenceFormationLevel",
      "/api/competenceFormationLevel/byCompetence",
      "/api/evaluationTool/type",
      "/api/literature",
      "/api/literature/byType",
      "/api/methodicalRecommendation",
      "/api/methodicalRecommendation/byProgram",
      "/api/reviewer",
      "/api/reviewer/byProgramId",
      "/api/audience",
      "/api/audience/byProgramId",
    ],
    target: "https://department-automation.herokuapp.com",
    secure: true,
    changeOrigin: true,
    logLevel: "debug",
  },
];

module.exports = PROXY_CONFIG;