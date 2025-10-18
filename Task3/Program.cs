

namespace task3
{
    // CLASS: Student
    class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; private set; } = new List<Course>();

        public bool Enroll(Course course)
        {
            bool alreadyEnrolled = false;
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseId == course.CourseId)
                {
                    alreadyEnrolled = true;
                    break;
                }
            }

            if (!alreadyEnrolled)
            {
                Courses.Add(course);
                return true;
            }
            return false;
        }

        public string PrintDetails()
        {
            string courseList = "";
            if (Courses.Count == 0)
            {
                courseList = "No courses enrolled.";
            }
            else
            {
                for (int i = 0; i < Courses.Count; i++)
                {
                    courseList += Courses[i].Title;
                    if (i < Courses.Count - 1)
                        courseList += ", ";
                }
            }
            return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseList}";
        }
    }

    //  CLASS: Instructor
    class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public string PrintDetails()
        {
            return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
        }
    }

    // CLASS: Course
    class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }

        public string PrintDetails()
        {
            string instructorName = Instructor != null ? Instructor.Name : "No instructor assigned";
            return $"ID: {CourseId}, Title: {Title}, Instructor: {instructorName}";
        }
    }

    //  CLASS: StudentManager
    class StudentManager
    {
        private List<Student> Students = new List<Student>();
        private List<Course> Courses = new List<Course>();
        private List<Instructor> Instructors = new List<Instructor>();

        public bool AddStudent(Student s)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == s.StudentId)
                    return false;
            }
            Students.Add(s);
            return true;
        }

        public bool AddInstructor(Instructor i)
        {
            for (int j = 0; j < Instructors.Count; j++)
            {
                if (Instructors[j].InstructorId == i.InstructorId)
                    return false;
            }
            Instructors.Add(i);
            return true;
        }

        public bool AddCourse(Course c)
        {
            for (int k = 0; k < Courses.Count; k++)
            {
                if (Courses[k].CourseId == c.CourseId)
                    return false;
            }
            Courses.Add(c);
            return true;
        }

        public Student FindStudent(string keyword)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId.ToString() == keyword || Students[i].Name.ToLower() == keyword.ToLower())
                    return Students[i];

            }
            return null;
        }

        public Course FindCourse(string keyword)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseId.ToString() == keyword || Courses[i].Title.ToLower() == keyword.ToLower())
                    return Courses[i];

            }
            return null;
        }

        public Instructor FindInstructor(string keyword)
        {
            for (int i = 0; i < Instructors.Count; i++)
            {
                if (Instructors[i].InstructorId.ToString() == keyword || Instructors[i].Name.Equals(keyword, StringComparison.OrdinalIgnoreCase))
                    return Instructors[i];
            }
            return null;
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student student = null;
            Course course = null;

            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == studentId)
                {
                    student = Students[i];
                    break;
                }
            }

            for (int j = 0; j < Courses.Count; j++)
            {
                if (Courses[j].CourseId == courseId)
                {
                    course = Courses[j];
                    break;
                }
            }

            if (student == null || course == null)
                return false;

            return student.Enroll(course);
        }

        public bool IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == studentId)
                {
                    for (int j = 0; j < Students[i].Courses.Count; j++)
                    {
                        if (Students[i].Courses[j].CourseId == courseId)
                            return true;
                    }
                }
            }
            return false;
        }

        public string GetInstructorNameByCourse(string courseName)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].Title.ToLower() == courseName.ToLower())
                {
                    if (Courses[i].Instructor != null)
                        return Courses[i].Instructor.Name;
                }

            }
            return "Instructor not found.";
        }


        public void ShowAllStudents()
        {
            if (Students.Count == 0)
                Console.WriteLine("No students available.");
            else
            {
                for (int i = 0; i < Students.Count; i++)
                    Console.WriteLine(Students[i].PrintDetails());
            }
        }

        public void ShowAllCourses()
        {
            if (Courses.Count == 0)
                Console.WriteLine("No courses available.");
            else
            {
                for (int i = 0; i < Courses.Count; i++)
                    Console.WriteLine(Courses[i].PrintDetails());
            }
        }

        public void ShowAllInstructors()
        {
            if (Instructors.Count == 0)
                Console.WriteLine("No instructors available.");
            else
            {
                for (int i = 0; i < Instructors.Count; i++)
                    Console.WriteLine(Instructors[i].PrintDetails());
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Student Management System ");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course (select instructor by ID)");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Show All Students");
                Console.WriteLine("6. Show All Courses");
                Console.WriteLine("7. Show All Instructors");
                Console.WriteLine("8. Find Student (by ID or Name)");
                Console.WriteLine("9. Find Course (by ID or Name)");
                Console.WriteLine("10. Check if Student Enrolled in Specific Course");
                Console.WriteLine("11. Get Instructor Name by Course Name");
                Console.WriteLine("12. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter ID: ");
                        int sid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        string sname = Console.ReadLine();
                        Console.Write("Enter Age: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        if (manager.AddStudent(new Student { StudentId = sid, Name = sname, Age = age }))
                            Console.WriteLine("Student added!");
                        else
                            Console.WriteLine("Student ID already exists!");
                        break;

                    case "2":
                        Console.Write("Enter ID: ");
                        int iid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        string iname = Console.ReadLine();
                        Console.Write("Enter Specialization: ");
                        string spec = Console.ReadLine();
                        if (manager.AddInstructor(new Instructor { InstructorId = iid, Name = iname, Specialization = spec }))
                            Console.WriteLine("Instructor added!");
                        else
                            Console.WriteLine("Instructor ID already exists!");
                        break;

                    case "3":
                        Console.Write("Enter Course ID: ");
                        int cid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Course Title: ");
                        string ctitle = Console.ReadLine();
                        Console.Write("Enter Instructor ID: ");
                        int insid = Convert.ToInt32(Console.ReadLine());
                        Instructor instr = manager.FindInstructor(insid.ToString());
                        if (instr == null)
                            Console.WriteLine("Instructor not found!");
                        else
                        {
                            if (manager.AddCourse(new Course { CourseId = cid, Title = ctitle, Instructor = instr }))
                                Console.WriteLine("Course added!");
                            else
                                Console.WriteLine("Course ID already exists!");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Student ID: ");
                        int sidEnroll = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int cidEnroll = Convert.ToInt32(Console.ReadLine());
                        if (manager.EnrollStudentInCourse(sidEnroll, cidEnroll))
                            Console.WriteLine("Student enrolled successfully!");
                        else
                            Console.WriteLine("Failed to enroll student.");
                        break;

                    case "5":
                        manager.ShowAllStudents();
                        break;

                    case "6":
                        manager.ShowAllCourses();
                        break;

                    case "7":
                        manager.ShowAllInstructors();
                        break;

                    case "8":
                        Console.Write("Enter Student ID or Name: ");
                        Student s = manager.FindStudent(Console.ReadLine());
                        if (s != null)
                            Console.WriteLine(s.PrintDetails());
                        else
                            Console.WriteLine("Student not found.");
                        break;

                    case "9":
                        Console.Write("Enter Course ID or Name: ");
                        Course c = manager.FindCourse(Console.ReadLine());
                        if (c != null)
                            Console.WriteLine(c.PrintDetails());
                        else
                            Console.WriteLine("Course not found.");
                        break;

                    case "10":
                        Console.Write("Enter Student ID: ");
                        int sidCheck = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int cidCheck = Convert.ToInt32(Console.ReadLine());
                        bool enrolled = manager.IsStudentEnrolledInCourse(sidCheck, cidCheck);
                        if (enrolled)
                            Console.WriteLine("the student is enrolled.");
                        else
                            Console.WriteLine("the student is not enrolled.");
                        break;

                    case "11":
                        Console.Write("Enter Course Name: ");
                        Console.WriteLine(manager.GetInstructorNameByCourse(Console.ReadLine()));
                        break;

                    case "12":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }


        }
    }
}
