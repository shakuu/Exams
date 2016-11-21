using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

using SchoolSystem.Cli.Configuration;
using SchoolSystem.Cli.ExecutionLoggers;
using SchoolSystem.Framework.Core;
using SchoolSystem.Framework.Core.Commands;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.CommandProviders;
using SchoolSystem.Framework.Core.CommandProviders.Contracts;
using SchoolSystem.Framework.Core.Contracts;
using SchoolSystem.Framework.Core.Factories;
using SchoolSystem.Framework.Core.Providers;

namespace SchoolSystem.Cli
{
    public class SchoolSystemModule : NinjectModule
    {
        private const string CreateStudentCommandName = "CreateStudentCommand";
        private const string CreateTeacherCommandName = "CreateTeacherCommand";
        private const string RemoveStudentCommandName = "RemoveStudentCommand";
        private const string RemoveTeacherCommandName = "RemoveTeacherCommand";
        private const string StudentListMarksCommandName = "StudentListMarksCommand";
        private const string TeacherAddMarkCommandName = "TeacherAddMarkCommand";

        private const string CreateStudentCommandProviderName = "CreateStudentCommandProvider";
        private const string CreateTeacherCommandProviderName = "CreateTeacherCommandProvider";
        private const string RemoveStudentCommandProviderName = "RemoveStudentCommandProvider";
        private const string RemoveTeacherCommandProviderName = "RemoveTeacherCommandProvider";
        private const string StudentListMarksProviderName = "StudentListMarksProvider";
        private const string TeacherAddMarkCommandProviderName = "TeacherAddMarkCommandProvider";

        public override void Load()
        {
            Kernel.Bind(context =>
            {
                context
                .FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            this.Bind<ICommandProviderChainOfResponsibility>().To<CreateStudentCommandProvider>().Named(CreateStudentCommandProviderName);
            this.Bind<ICommandProviderChainOfResponsibility>().To<CreateTeacherCommandProvider>().Named(CreateTeacherCommandProviderName);
            this.Bind<ICommandProviderChainOfResponsibility>().To<RemoveStudentCommandProvider>().Named(RemoveStudentCommandProviderName);
            this.Bind<ICommandProviderChainOfResponsibility>().To<RemoveTeacherCommandProvider>().Named(RemoveTeacherCommandProviderName);
            this.Bind<ICommandProviderChainOfResponsibility>().To<StudentListMarksProvider>().Named(StudentListMarksProviderName);
            this.Bind<ICommandProviderChainOfResponsibility>().To<TeacherAddMarkCommandProvider>().Named(TeacherAddMarkCommandProviderName);
            this.Bind<ICommandProvider>().ToMethod(ctx =>
            {
                var createStudent = ctx.Kernel.Get<ICommandProviderChainOfResponsibility>(CreateStudentCommandProviderName);
                var createTeacher = ctx.Kernel.Get<ICommandProviderChainOfResponsibility>(CreateTeacherCommandProviderName);
                var removeStudent = ctx.Kernel.Get<ICommandProviderChainOfResponsibility>(RemoveStudentCommandProviderName);
                var removeTeacher = ctx.Kernel.Get<ICommandProviderChainOfResponsibility>(RemoveTeacherCommandProviderName);
                var studentListMarks = ctx.Kernel.Get<ICommandProviderChainOfResponsibility>(StudentListMarksProviderName);
                var teacherAddMark = ctx.Kernel.Get<ICommandProviderChainOfResponsibility>(TeacherAddMarkCommandProviderName);

                createStudent.SetNextElement(createTeacher);
                createTeacher.SetNextElement(removeStudent);
                removeStudent.SetNextElement(removeTeacher);
                removeTeacher.SetNextElement(studentListMarks);
                studentListMarks.SetNextElement(teacherAddMark);

                return createStudent;
            });

            this.Bind(typeof(ISchoolSystemData), typeof(IStudentData), typeof(ITeachersData))
                .To<ComposedSchoolSystemData>().InSingletonScope();

            this.Bind<IIdentityProvider>().To<SchoolSystemIdentityProvider>()
                .WhenInjectedInto<CreateStudentCommand>().InSingletonScope();

            this.Bind<IIdentityProvider>().To<SchoolSystemIdentityProvider>()
               .WhenInjectedInto<CreateTeacherCommand>().InSingletonScope();

            this.Bind<ICommand>().To<CreateStudentCommand>().InSingletonScope().Named(CreateStudentCommandName);
            this.Bind<ICommand>().To<CreateTeacherCommand>().InSingletonScope().Named(CreateTeacherCommandName);
            this.Bind<ICommand>().To<RemoveStudentCommand>().InSingletonScope().Named(RemoveStudentCommandName);
            this.Bind<ICommand>().To<RemoveTeacherCommand>().InSingletonScope().Named(RemoveTeacherCommandName);
            this.Bind<ICommand>().To<StudentListMarksCommand>().InSingletonScope().Named(StudentListMarksCommandName);
            this.Bind<ICommand>().To<TeacherAddMarkCommand>().InSingletonScope().Named(TeacherAddMarkCommandName);

            this.Bind<IReader>().To<ConsoleReaderProvider>();
            this.Bind<IWriter>().To<ConsoleWriterProvider>();
            this.Bind<IParser>().To<CommandParserProvider>();

            this.Bind<ITeacherFactory>().ToFactory().InSingletonScope();
            var studentFactoryBinding = this.Bind<IStudentFactory>().ToFactory().InSingletonScope();
            var commandFactoryBinding = this.Bind<ICommandFactory>().ToFactory().InSingletonScope();
            var markFactoryBinding = this.Bind<IMarkFactory>().ToFactory().InSingletonScope();

            var getCommandBinding = this.Bind<ICommand>().ToMethod(context =>
            {
                var commandName = (string)context.Parameters.FirstOrDefault().GetValue(context, null);
                if (commandName == null)
                {
                    throw new ArgumentException("The passed command is not found!");
                }

                try
                {
                    var command = context.Kernel.Get<ICommand>(commandName + "Command");
                    return command;
                }
                catch (Exception inner)
                {
                    throw new ArgumentException("The passed command is not found!", inner);
                }
            })
            .NamedLikeFactoryMethod((ICommandFactory factory) => factory.GetCommand(null));

            IConfigurationProvider configurationProvider = Kernel.Get<IConfigurationProvider>();
            if (configurationProvider.IsTestEnvironment)
            {
                commandFactoryBinding.Intercept().With<ExecutionTimeLoggingInterceptor>();
                studentFactoryBinding.Intercept().With<ExecutionTimeLoggingInterceptor>();
                markFactoryBinding.Intercept().With<ExecutionTimeLoggingInterceptor>();

                // Uncomment this to remove any output other than the time elapsed strings.
                //this.Kernel.InterceptReplace<ConsoleWriterProvider>(c => c.WriteLine(null), invocation => { });
                //this.Kernel.InterceptReplace<ConsoleWriterProvider>(c => c.Write(null), invocation => { });
            }
        }
    }
}