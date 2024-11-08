using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace numericMethodsProgram
{
    public partial class Matrix : Form
    {
        int oldWidth1; int oldHeight1;
        int oldWidth2; int oldHeight2;
        public Matrix()
        {
            InitializeComponent();
            oldWidth1 = (int)numRows.Value;
            oldHeight1 = (int)numCols.Value;
            oldWidth2 = (int)numRows2.Value;
            oldHeight2 = (int)numCols2.Value;
            Size baseSize = new Size(this.Width, this.Height);
        }

        private void Form5_Load_1(object sender, EventArgs e)
        {
            // Initialize DataGridViews for Matrix 1 and Matrix 2
            InitializeMatrix(dataGridView1, (int)numRows.Value, (int)numCols.Value);
            InitializeMatrix(dataGridView2, (int)numRows.Value, (int)numCols.Value);
        }

        private void numRows_ValueChanged(object sender, EventArgs e)
        {
            // Adjust number of rows for both matrices when user changes the row count
            AdjustMatrixSize(dataGridView1, (int)numRows.Value, (int)numCols.Value);
            if ((int)numRows.Value > oldHeight1)
            {
                if ((int)numRows.Value > oldHeight2)
                {
                    if ((int)numRows.Value > 4)
                    {
                        this.Size = new Size(this.Width, this.Height + (33));
                    }
                }
            }
            else
            {
                if ((int)numRows.Value >= oldHeight2)
                {
                    if ((int)numRows.Value >= 4)
                    {
                        this.Size = new Size(this.Width, this.Height - (33));
                    }
                }
            }
            oldHeight1 = (int)numRows.Value;
        }

        private void numRows2_ValueChanged(object sender, EventArgs e)
        {
            AdjustMatrixSize(dataGridView2, (int)numRows2.Value, (int)numCols2.Value);
            if ((int)numRows2.Value > oldHeight2)
            {
                if ((int)numRows2.Value > oldHeight1)
                {
                    if ((int)numRows2.Value > 4)
                    {
                        this.Size = new Size(this.Width, this.Height + (33));
                    }
                }
            }
            else
            {
                if ((int)numRows2.Value >= oldHeight1)
                {
                    if ((int)numRows2.Value >= 4)
                    {
                        this.Size = new Size(this.Width, this.Height - (33));
                    }
                }
            }
            oldHeight2 = (int)numRows2.Value;

        }

        private void numCols_ValueChanged(object sender, EventArgs e)
        {
            // Adjust number of columns for both matrices when user changes the column count
            AdjustMatrixSize(dataGridView1, (int)numRows.Value, (int)numCols.Value);
            if ((int)numCols.Value > oldWidth1)
            {
                if ((int)numCols.Value > 4)
                {
                    this.Size = new Size(this.Width + (50), this.Height);
                    dataGridView2.Left += (50);
                    dataGridView3.Left += (50);
                    label2.Left += 50; label3.Left += 50;
                }
            }
            else
            {
                if ((int)numCols.Value >= 4)
                {
                    this.Size = new Size(this.Width - (50), this.Height);
                    dataGridView2.Left -= (50);
                    dataGridView3.Left -= (50);
                    label2.Left -= (50); label3.Left -= (50);
                }
            }
            oldWidth1 = (int)numCols.Value;
        }

        private void numCols2_ValueChanged(object sender, EventArgs e)
        {
            AdjustMatrixSize(dataGridView2, (int)numRows2.Value, (int)numCols2.Value);
            if ((int)numCols2.Value > oldWidth2)
            {
                if ((int)numCols2.Value > 4)
                {
                    this.Size = new Size(this.Width + (50), this.Height);
                    dataGridView3.Left += (50);
                    label3.Left += 50;
                }
            }
            else
            {
                if ((int)numCols2.Value >= 4)
                {
                    this.Size = new Size(this.Width - (50), this.Height);
                    dataGridView3.Left -= (50);
                    label3.Left -= (50);
                }
            }
            oldWidth2 = (int)numCols2.Value;
        }


        private void InitializeMatrix(DataGridView dgv, int rows, int cols)
        {
            dgv.ColumnCount = cols;
            dgv.RowCount = rows;
            for (int i = 0; i < cols; i++)
            {
                dgv.Columns[i].Width = 50;
            }
        }

        private void AdjustMatrixSize(DataGridView dgv, int rows, int cols)
        {
            dgv.ColumnCount = cols;
            dgv.RowCount = rows;
            int rowHeight = dgv.RowTemplate.Height;
            int colWidth = 50;

            int totalWidth = dgv.RowHeadersVisible ? dgv.RowHeadersWidth : 0;
            totalWidth += dgv.Columns.Count * colWidth;

            int totalHeight = dgv.ColumnHeadersVisible ? dgv.ColumnHeadersHeight : 0;
            totalHeight += dgv.RowCount * rowHeight;

            dgv.Width = totalWidth + 3;
            dgv.Height = totalHeight + 3;



        }

        private void bttnAdd_Click(object sender, EventArgs e)
        {
            // Perform matrix addition
            PerformOperation(MatrixOperation.Add);
        }

        private void bttnSubtract_Click(object sender, EventArgs e)
        {
            // Perform matrix subtraction
            PerformOperation(MatrixOperation.Subtract);
        }

        private void bttnMultiply_Click(object sender, EventArgs e)
        {
            // Perform matrix multiplication
            PerformOperation(MatrixOperation.Multiply);
        }

        private void PerformOperation(MatrixOperation operation)
        {
            int rows = (int)numRows.Value;
            int cols = (int)numCols.Value;
            double[,] matrix1 = GetMatrixFromDataGridView(dataGridView1);
            double[,] matrix2 = GetMatrixFromDataGridView(dataGridView2);
            double[,] resultMatrix = null;
            try
            {
                switch (operation)
                {
                    case MatrixOperation.Add:
                        resultMatrix = AddMatrices(matrix1, matrix2);
                        break;
                    case MatrixOperation.Subtract:
                        resultMatrix = SubtractMatrices(matrix1, matrix2);
                        break;
                    case MatrixOperation.Multiply:
                        resultMatrix = MultiplyMatrices(matrix1, matrix2);
                        break;
                }
                DisplayResult(resultMatrix);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double[,] GetMatrixFromDataGridView(DataGridView dgv)
        {
            int rows = dgv.RowCount;
            int cols = dgv.ColumnCount;
            double[,] matrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = Convert.ToDouble(dgv[j, i].Value);
                }
            }

            return matrix;
        }

        private double[,] AddMatrices(double[,] matrix1, double[,] matrix2)
        {
            int rows = matrix1.GetLength(0);
            int cols = matrix1.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return result;
        }

        private double[,] SubtractMatrices(double[,] matrix1, double[,] matrix2)
        {
            int rows = matrix1.GetLength(0);
            int cols = matrix1.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            return result;
        }

        private double[,] ScaleMatrix(double[,] matrix, double scalar)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix[i, j] * scalar;
                }
            }

            return result;
        }

        private double[,] TransposeMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix[j, i];
                }
            }

            return result;
        }

        private double[,] MultiplyMatrices(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int cols2 = matrix2.GetLength(1);

            if (cols1 != rows2)
            {
                throw new Exception("Number of columns in Matrix 1 must match number of rows in Matrix 2.");
            }

            double[,] result = new double[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    for (int k = 0; k < cols1; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return result;
        }

        double MatrixDet(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double result;

            if (rows == 2)
            {
                return (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
            }
            else if (rows == 3)
            {
                return ((((matrix[0, 0] * matrix[1, 1] * matrix[2, 2]) + (matrix[0, 1] * matrix[1, 2] * matrix[2, 0]) + (matrix[0, 2] * matrix[1, 0] * matrix[2, 1])) - ((matrix[2, 0] * matrix[1, 1] * matrix[0, 2]) + (matrix[2, 1] * matrix[1, 2] * matrix[0, 0]) + (matrix[2, 2] * matrix[1, 0] * matrix[0, 1]))));
            }
            else
            {
                return 0;
            }


        }

        private void DisplayResult(double[,] result)
        {
            // Display result matrix in DataGridView3
            dataGridView3.ColumnCount = result.GetLength(1);
            dataGridView3.RowCount = result.GetLength(0);
            Size current = new Size(this.Width, this.Height);

            AdjustMatrixSize(dataGridView3, (int)dataGridView3.RowCount, (int)dataGridView3.ColumnCount);

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    dataGridView3[j, i].Value = result[i, j];

                }
            }

            if (dataGridView3.ColumnCount > 4)
            {
                this.Size = new Size(current.Width + (50 * (result.GetLength(1) - 4)), current.Height);
            }
            if (dataGridView3.RowCount > 4)
            {
                this.Size = new Size(current.Width, current.Height + (50 * (50 * (result.GetLength(0) - 4))));
            }
        }

        private void bttnScalar_Click(object sender, EventArgs e)
        {
            double[,] matrix1 = GetMatrixFromDataGridView(dataGridView1);
            double[,] matrix2 = GetMatrixFromDataGridView(dataGridView2);

            if (!int.TryParse(tbScale.Text, out int number))
            {
                MessageBox.Show("Por favor, coloque un valor numérico válido para el escalar", "Escalar no declarado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (radioButton1.Checked)
                {
                    DisplayResult(ScaleMatrix(matrix1, int.Parse(tbScale.Text)));
                }
                else if (radioButton2.Checked)
                {
                    DisplayResult(ScaleMatrix(matrix2, int.Parse(tbScale.Text)));
                }
                else
                {
                    MessageBox.Show("Por favor, elija qué matriz desea escalar", "Matriz no definida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private double Determinant(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double det = 1;
            double[,] tempMatrix = (double[,])matrix.Clone();

            for (int i = 0; i < n; i++)
            {
                if (tempMatrix[i, i] == 0)
                {
                    bool isSwapped = false;
                    for (int j = i + 1; j < n; j++)
                    {
                        if (tempMatrix[j, i] != 0)
                        {
                            SwapRows(tempMatrix, i, j);
                            det *= -1;
                            isSwapped = true;
                            break;
                        }
                    }
                    if (!isSwapped)
                    {
                        return 0;
                    }
                }
                for (int j = i + 1; j < n; j++)
                {
                    double factor = tempMatrix[j, i] / tempMatrix[i, i];
                    for (int k = i; k < n; k++)
                    {
                        tempMatrix[j, k] -= factor * tempMatrix[i, k];
                    }
                }
                det *= tempMatrix[i, i];
            }
            return det;
        }

        private double[,] InverseMatrix(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] augmentedMatrix = new double[n, 2 * n];
            double[,] result = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = matrix[i, j];
                }
                augmentedMatrix[i, n + i] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                if (augmentedMatrix[i, i] == 0)
                {
                    bool isSwapped = false;
                    for (int k = i + 1; k < n; k++)
                    {
                        if (augmentedMatrix[k, i] != 0)
                        {
                            SwapRows(augmentedMatrix, i, k);
                            isSwapped = true;
                            break;
                        }
                    }
                    if (!isSwapped)
                    {
                        throw new Exception("La matriz no es invertible");
                    }
                }
                double pivot = augmentedMatrix[i, i];
                for (int j = 0; j < 2 * n; j++)
                {
                    augmentedMatrix[i, j] /= pivot;
                }
                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        double factor = augmentedMatrix[k, i];
                        for (int j = 0; j < 2 * n; j++)
                        {
                            augmentedMatrix[k, j] -= factor * augmentedMatrix[i, j];
                        }
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = augmentedMatrix[i, n + j];
                }
            }
            return result;
        }

        private void SwapRows(double[,] matrix, int row1, int row2)
        {
            int cols = matrix.GetLength(1);
            for (int i = 0; i < cols; i++)
            {
                double temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = temp;
            }
        }



        private void bttnTranpose_Click(object sender, EventArgs e)
        {
            double[,] matrix1 = GetMatrixFromDataGridView(dataGridView1);
            double[,] matrix2 = GetMatrixFromDataGridView(dataGridView2);
            if (radioButton3.Checked)
            {
                DisplayResult(TransposeMatrix(matrix1));
            }
            else if (radioButton4.Checked)
            {
                DisplayResult(TransposeMatrix(matrix2));
            }
            else
            {
                MessageBox.Show("Por favor, elija qué matriz desea Transponer", "Matriz no definida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttnInverse_Click(object sender, EventArgs e)
        {
            double[,] matrix1 = GetMatrixFromDataGridView(dataGridView1);
            double[,] matrix2 = GetMatrixFromDataGridView(dataGridView2);
            if (radioButton3.Checked)
            {
                if (Determinant(matrix1) != 0)
                {
                    DisplayResult(InverseMatrix(matrix1));
                }
                else MessageBox.Show("La Matriz puesta no es invertible", "No invertible", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton4.Checked)
            {
                if (Determinant(matrix1) != 0)
                {
                    DisplayResult(InverseMatrix(matrix2));
                }
                else MessageBox.Show("La Matriz puesta no es invertible", "No invertible", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Por favor, elija qué matriz desea invetir", "Matriz no definida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    enum MatrixOperation
    {
        Add,
        Subtract,
        Multiply
    }
}