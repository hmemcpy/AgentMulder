using AgentMulder.TypeSystem.Adapters;
using JetBrains.ProjectModel;
using Machine.Fakes;
using Machine.Specifications;
using ISolution = AgentMulder.TypeSystem.ProjectModel.ISolution;

namespace AgentMulder.Specs
{
    using JbISolution = JetBrains.ProjectModel.ISolution;
    using JbIProject = JetBrains.ProjectModel.IProject;

    [Subject("Adapting JetBrains project model")]
    public class jetbrains_project_model_adaptation : WithFakes
    {
        protected static JbISolution fakeJbISolution;
        protected static JbIProject fakeJbProject;
        protected static SolutionAdapter solutionAdapter;
        protected static ISolution solution;

        Establish context = () =>
        {
            fakeJbISolution = An<JbISolution>();
            fakeJbISolution.WhenToldTo(s => s.Name).Return("Solution");
            solutionAdapter = new SolutionAdapter(fakeJbISolution);

            fakeJbProject = An<JbIProject>();
            fakeJbProject.WhenToldTo(project => project.Name).Return("Project");
        };
    }

    public class when_adapting_empty_solution : jetbrains_project_model_adaptation
    {
        static JbIProject fakeJbProject;

        Because of = () => solution = solutionAdapter.CreateSolution();

        It should_have_no_projects = () => solution.Projects.ShouldBeEmpty();
        It should_have_a_title_of_the_original_solution = () => solution.Name.ShouldEqual("Solution");
    }

    public class when_adapting_solution_with_one_project : jetbrains_project_model_adaptation
    {
        Establish context = () => fakeJbISolution.WhenToldTo(s => s.GetAllProjects())
                                      .Return(() => new[] { fakeJbProject });

        Because of = () => solution = solutionAdapter.CreateSolution();

        It should_have_one_project = () => solution.Projects.Count.ShouldEqual(1);
    }

    public class when_project_contans_assembly_references : jetbrains_project_model_adaptation
    {
        Establish context = () => 
        {
            var fakeProjectToAssemblyReference = An<IProjectToAssemblyReference>();
            fakeProjectToAssemblyReference.WhenToldTo(reference => reference.Name).Return("TestLibrary");
            fakeJbProject.WhenToldTo(project => project.GetAssemblyReferences()).Return(new[] { fakeProjectToAssemblyReference });
        };

        Because of = () => solution = solutionAdapter.CreateSolution();

        It should_have_one_assembly_reference_in_project =
            () => solution.Projects[0].AssemblyReferences.Count.ShouldEqual(1);
    }
}