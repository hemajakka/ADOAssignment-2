using System.Data.SqlClient;
using System.Data;

namespace AdoAssignment2
{
    class Note
    {
      public void AddNote(SqlConnection conn)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from note", conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var row = ds.Tables[0].NewRow();
            Console.WriteLine("enter title");
            row["title"]=Console.ReadLine();
            Console.WriteLine("enter description");
            row["Descriptions"]=Console.ReadLine();
            Console.WriteLine("enter date");
            row["datee"]=Console.ReadLine();
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("database updated");


        }
        public void ViewNote(SqlConnection conn)
        { 
            Console.WriteLine("enter the id to view");
            int id=Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"select * from note where id={id}", conn);
            DataSet ds = new DataSet();
            adp.Fill(ds, "note");
            for (int i = 0; i < ds.Tables["note"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["note"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["note"].Rows[i][j]} |");
                }
            }
            Console.WriteLine();
        }
        public void ViewAllNote(SqlConnection conn)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from note", conn);
            DataSet ds = new DataSet();
            adp.Fill(ds,"note");
            for(int i=0; i < ds.Tables["note"].Rows.Count;i++)
            {
                for(int j = 0; j < ds.Tables["note"].Columns.Count;j++)
                {
                    Console.Write($"{ds.Tables["note"].Rows[i][j]} |");
                }
                Console.WriteLine();
            }
          
            
            Console.WriteLine($"Total notes are: {ds.Tables["note"].Rows.Count}");

        }
        public void UpdateNote(SqlConnection conn) 
        {
            Console.WriteLine("enter the noteid to update");
            int id= Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"select * from note where id={id}", conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Console.WriteLine("enter the column name you want to upadate");
            string colname=Console.ReadLine();
            Console.WriteLine("enter the row index you want to update");
            int index= Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("enter the update value");
            string value=Console.ReadLine();
            ds.Tables[0].Rows[index][colname] = value;
            SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("note updated successfully");
        }
        public void DeleteNote(SqlConnection conn)
        {
            Console.WriteLine("enter the note id to delete note");
            int id=Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"select * from note where id={id}", conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Console.WriteLine("enter row index you want to delete");
            int index=Convert.ToInt16(Console.ReadLine());
            ds.Tables[0].Rows[index].Delete();
            SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("note deleted successfully");

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("server=IN-9SB79S3;database=keepnotedb;Integrated Security=true");
            string ans = " ";
            Note note = new Note();
            do
            {
                Console.WriteLine("welcome to view note app");
                Console.WriteLine("enter your choice");
                Console.WriteLine("1.add note");
                Console.WriteLine("2.view note");
                Console.WriteLine("3.view all notes");
                Console.WriteLine("4.update note");
                Console.WriteLine("5.delete note");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            note.AddNote(conn);
                            break;
                        }
                    case 2:
                        {
                            note.ViewNote(conn);
                            break;
                        }
                    case 3:
                        {
                            note.ViewAllNote(conn);

                            break;
                        }
                    case 4:
                        {
                            note.UpdateNote(conn);
                            break;
                        }
                    case 5:
                        {
                            note.DeleteNote(conn);
                            break;
                        }

                }
                Console.WriteLine("do you want to continue[y/n]");
                ans = Console.ReadLine();
            } while (ans.ToLower()=="y");
        }
    }
}