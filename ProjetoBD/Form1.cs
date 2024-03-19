using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoBD {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        SQLiteConnection conexao;

        public SQLiteConnection ObterConexao() {
            conexao = new SQLiteConnection("Data Source=C:\\Users\\vitorya.candido\\source\\repos\\ProjetoBD\\database.db");
            conexao.Open();
            return conexao;
        }

        public DataTable ObterProdutos() {
            SQLiteDataAdapter da;
            DataTable dt = new DataTable();

            var cmd = ObterConexao().CreateCommand();
            cmd.CommandText = "SELECT * FROM produtos WHERE id='" + textBox1.Text + "'"; // pega o id digitado pelo user


            da = new SQLiteDataAdapter(cmd.CommandText, ObterConexao());


            da.Fill(dt);

            return dt;
        }


        private void button1_Click(object sender, EventArgs e) {
            DataTable dt = ObterProdutos();
            if (dt.Rows.Count > 0) {
                string dado = dt.Rows[0].ItemArray[1].ToString(); //mostra o nome 
                MessageBox.Show(dado);
            }
            else {
                MessageBox.Show("Nenhum produto encontrado com o ID especificado.");
            }
        }



        private void button2_Click(object sender, EventArgs e) {
            var cmd = ObterConexao().CreateCommand();
            cmd.CommandText = "INSERT INTO produtos(id, nome, preco) VALUES (@id,@nome,@preco)";
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@nome", textBox2.Text);
            cmd.Parameters.AddWithValue("@preco", textBox3.Text);

            cmd.ExecuteNonQuery();
            ObterConexao().Close();
            MessageBox.Show("NOVO PRODUTO ADICIONADO");
        }

        private void button3_Click(object sender, EventArgs e) {
            var cmd = ObterConexao().CreateCommand();
            cmd.CommandText = "DELETE FROM produtos WHERE id='" + textBox1.Text + "'";
            

            cmd.ExecuteNonQuery();
            ObterConexao().Close();
            MessageBox.Show("PRODUTO DELETADO");
        }
    }
}
