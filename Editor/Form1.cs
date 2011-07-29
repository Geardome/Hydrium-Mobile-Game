using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Editor
{
    public partial class MainForm : Form
    {
        
        EditorManager m_EditorManager = new EditorManager();        


        //--------------------------------------------------------------------
        // Funci�n:    MainForm
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:09:20
        //--------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();
        }


        //--------------------------------------------------------------------
        // Funci�n:    MainForm_Load
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:09:32
        //--------------------------------------------------------------------
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            m_EditorManager.Start(this);   
        }


        //--------------------------------------------------------------------
        // Funci�n:    pictureBoxRendering_MouseEnter
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:09:41
        //--------------------------------------------------------------------
        private void pictureBoxRendering_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
            m_EditorManager.ShowItemCursor = true;
        }


        //--------------------------------------------------------------------
        // Funci�n:    pictureBoxRendering_MouseLeave
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:09:50
        //--------------------------------------------------------------------
        private void pictureBoxRendering_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            m_EditorManager.ShowItemCursor = false;
            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    pictureBoxRendering_MouseMove
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:10:05
        //--------------------------------------------------------------------
        private void pictureBoxRendering_MouseMove(object sender, MouseEventArgs e)
        {
            m_EditorManager.MouseMove(e.Location);

            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    pictureBoxRendering_Paint
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:10:14
        //--------------------------------------------------------------------
        private void pictureBoxRendering_Paint(object sender, PaintEventArgs e)
        {
            m_EditorManager.Render(e.Graphics);                        
        }


        //--------------------------------------------------------------------
        // Funci�n:    radioCubos_CheckedChanged
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:10:23
        //--------------------------------------------------------------------
        private void radioCubos_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCubos.Checked)
            {
                listViewTextures.Clear();
                listViewTextures.LargeImageList = imageListCubos;

                for (int i = 0; i < m_EditorManager.m_ImagenesCubos.Length; i++)
                {
                    listViewTextures.Items.Add(new ListViewItem("", i));    
                }                
            }
        }


        //--------------------------------------------------------------------
        // Funci�n:    radioTransport_CheckedChanged
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:10:40
        //--------------------------------------------------------------------
        private void radioTransport_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTransport.Checked)
            {
                listViewTextures.Clear();
                listViewTextures.LargeImageList = imageListTrans;

                listViewTextures.Items.Add(new ListViewItem("", 0));
                 
            }
        }


        //--------------------------------------------------------------------
        // Funci�n:    radioEnemigos_CheckedChanged
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:10:48
        //--------------------------------------------------------------------
        private void radioEnemigos_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEnemigos.Checked)
            {
                listViewTextures.Clear();
                listViewTextures.LargeImageList = imageListEnems;

                for (int i = 0; i < EditorManager.NUM_ENEMS; i++)
                {
                    listViewTextures.Items.Add(new ListViewItem("", i));
                }
            }
        }


        //--------------------------------------------------------------------
        // Funci�n:    radioItems_CheckedChanged
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:10:59
        //--------------------------------------------------------------------
        private void radioItems_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItems.Checked)
            {
                listViewTextures.Clear();
                listViewTextures.LargeImageList = imageListItems;

                for (int i = 0; i < EditorManager.NUM_ITEMS; i++)
                {
                    listViewTextures.Items.Add(new ListViewItem("", i));
                }
            }
        }


        //--------------------------------------------------------------------
        // Funci�n:    horizontalScrollBar_Scroll
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:11:06
        //--------------------------------------------------------------------
        private void horizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            m_EditorManager.CameraCoordinateX = e.NewValue;
            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    verticalScrollBar_Scroll
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:11:15
        //--------------------------------------------------------------------
        private void verticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            m_EditorManager.CameraCoordinateY = e.NewValue;
            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    pictureBoxRendering_MouseDown
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:11:23
        //--------------------------------------------------------------------
        private void pictureBoxRendering_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBoxPantalla.Checked)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (radioCubos.Checked)
                {
                    if (listViewTextures.SelectedIndices.Count == 1)
                    {
                        if (numericUpDownCursorX.Value > 1)
                        {
                            for (int i = 0; i < numericUpDownCursorX.Value; i++)
                            {
                                switch (comboBoxPiso.SelectedIndex)
                                {
                                    case 0:
                                    {
                                        m_EditorManager.AddObject(0, (byte)listViewTextures.SelectedIndices[0], 0, 4 * i, 0, m_EditorManager.MouseRealCoordinates);
                                        break;
                                    }
                                    case 1:                                   
                                    case 2:
                                    {
                                        m_EditorManager.AddCubeLevel(0, (byte)listViewTextures.SelectedIndices[0], 0, 4 * i, 0, m_EditorManager.MouseRealCoordinates, comboBoxPiso.SelectedIndex);
                                        break;
                                    }
                                }
                                
                            }
                        }
                        else if (numericUpDownCursorY.Value > 1)
                        {
                            for (int i = 0; i < numericUpDownCursorY.Value; i++)
                            {
                                switch (comboBoxPiso.SelectedIndex)
                                {
                                    case 0:
                                    {
                                        m_EditorManager.AddObject(0, (byte)listViewTextures.SelectedIndices[0], 0, 0, 4 * i, m_EditorManager.MouseRealCoordinates);
                                        break;
                                    }
                                    case 1:
                                    case 2:
                                    {
                                        m_EditorManager.AddCubeLevel(0, (byte)listViewTextures.SelectedIndices[0], 0, 0, 4 * i, m_EditorManager.MouseRealCoordinates, comboBoxPiso.SelectedIndex);
                                        break;
                                    }
                                }                                
                            }
                        }
                        else
                        {
                            switch (comboBoxPiso.SelectedIndex)
                            {
                                case 0:
                                {
                                    m_EditorManager.AddObject(0, (byte)listViewTextures.SelectedIndices[0], 0, 0, 0, m_EditorManager.MouseRealCoordinates);
                                    break;
                                }
                                case 1:
                                case 2:
                                {
                                    m_EditorManager.AddCubeLevel(0, (byte)listViewTextures.SelectedIndices[0], 0, 0, 0, m_EditorManager.MouseRealCoordinates, comboBoxPiso.SelectedIndex);
                                    break;
                                }
                            }                             
                        }

                        pictureBoxRendering.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un cubo.", "Selecciona un cubo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (radioItems.Checked)
                {
                    if (listViewTextures.SelectedIndices.Count == 1)
                    {
                        if (numericUpDownCursorX.Value > 1)
                        {
                            for (int i = 0; i < numericUpDownCursorX.Value; i++)
                            {
                                m_EditorManager.AddObject(1, (byte)listViewTextures.SelectedIndices[0], m_EditorManager.Rotation, 4 * i, 0, m_EditorManager.MouseRealCoordinates);
                            }
                        }
                        else if (numericUpDownCursorY.Value > 1)
                        {
                            for (int i = 0; i < numericUpDownCursorY.Value; i++)
                            {
                                m_EditorManager.AddObject(1, (byte)listViewTextures.SelectedIndices[0], m_EditorManager.Rotation, 0, 4 * i, m_EditorManager.MouseRealCoordinates);
                            }
                        }
                        else                           
                        {
                            m_EditorManager.AddObject(1, (byte)listViewTextures.SelectedIndices[0], m_EditorManager.Rotation, 0, 0, m_EditorManager.MouseRealCoordinates);
                        }

                        pictureBoxRendering.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un item.", "Selecciona un item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (radioEnemigos.Checked)
                {
                    if (listViewTextures.SelectedIndices.Count == 1)
                    {
                        if (numericUpDownCursorX.Value > 1)
                        {
                            for (int i = 0; i < numericUpDownCursorX.Value; i++)
                            {
                                m_EditorManager.AddObject(2, (byte)listViewTextures.SelectedIndices[0], m_EditorManager.Rotation, 4 * i, 0, m_EditorManager.MouseRealCoordinates);
                            }
                        }
                        else if (numericUpDownCursorY.Value > 1)
                        {
                            for (int i = 0; i < numericUpDownCursorY.Value; i++)
                            {
                                m_EditorManager.AddObject(2, (byte)listViewTextures.SelectedIndices[0], m_EditorManager.Rotation, 0, 4 * i, m_EditorManager.MouseRealCoordinates);
                            }
                        }
                        else
                        {
                            m_EditorManager.AddObject(2, (byte)listViewTextures.SelectedIndices[0], m_EditorManager.Rotation, 0, 0, m_EditorManager.MouseRealCoordinates);
                        }

                        pictureBoxRendering.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un enemigo.", "Selecciona un enemigo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (radioTransport.Checked)
                {
                    if (listViewTextures.SelectedIndices.Count == 1)
                    {
                        m_EditorManager.AddObject(3, (byte)listViewTextures.SelectedIndices[0], 100, 0, 0, m_EditorManager.MouseRealCoordinates);
                        
                        pictureBoxRendering.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Selecciona el teletransportador.", "Selecciona el teletransportador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            
            if (e.Button == MouseButtons.Right)
            {
                if (radioCubos.Checked)
                {
                    m_EditorManager.DeleteObject(0);
                    pictureBoxRendering.Invalidate();
                }
                else if (radioItems.Checked)
                {
                    m_EditorManager.DeleteObject(1);
                    pictureBoxRendering.Invalidate();
                }
                else if (radioEnemigos.Checked)
                {
                    m_EditorManager.DeleteObject(2);
                    pictureBoxRendering.Invalidate();
                }
                else if (radioTransport.Checked)
                {
                    m_EditorManager.DeleteObject(3);
                    pictureBoxRendering.Invalidate();
                }
            }
        }        


        //--------------------------------------------------------------------
        // Funci�n:    checkBoxPantalla_CheckedChanged
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 9:11:40
        //--------------------------------------------------------------------
        private void checkBoxPantalla_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    abrirToolStripMenuItem_Click
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 11:39:04
        //--------------------------------------------------------------------
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();         
        }
        

        //--------------------------------------------------------------------
        // Funci�n:    saveFileDialog_FileOk
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 11:44:15
        //--------------------------------------------------------------------
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            this.Text = "Hydrium Mobile Editor - " + saveFileDialog.FileName;

            m_EditorManager.SaveFile(saveFileDialog.OpenFile());
        }


        //--------------------------------------------------------------------
        // Funci�n:    guardarComoToolStripMenuItem_Click
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 11:48:46
        //--------------------------------------------------------------------
        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_EditorManager.m_iTeleport % 2 == 1)
            {
                MessageBox.Show("Hay un teletransportador sin pareja.");

                return;
            }

            saveFileDialog.ShowDialog();
        }


        //--------------------------------------------------------------------
        // Funci�n:    nuevoToolStripMenuItem_Click
        // Prop�sito:  
        // Fecha:      mi�rcoles, 25 de enero de 2006, 12:06:41
        //--------------------------------------------------------------------
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Hydrium Mobile Editor";
            m_EditorManager.ResetScene();
            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    buttonRotacion_Click
        // Prop�sito:  
        // Fecha:      lunes, 30 de enero de 2006, 16:07:34
        //--------------------------------------------------------------------
        private void buttonRotacion_Click(object sender, EventArgs e)
        {
            m_EditorManager.Rotation++;

            m_EditorManager.Rotation %= 4;

            if (m_EditorManager.Rotation == 0)
            {
                buttonRotacion.Image = global::Editor.Properties.Resources.flecha1;
            }
            else if (m_EditorManager.Rotation == 1)
            {
                buttonRotacion.Image = global::Editor.Properties.Resources.flecha2;
            }
            else if (m_EditorManager.Rotation == 2)
            {
                buttonRotacion.Image = global::Editor.Properties.Resources.flecha3;
            }
            else if (m_EditorManager.Rotation == 3)
            {
                buttonRotacion.Image = global::Editor.Properties.Resources.flecha4;
            }
        }


        //--------------------------------------------------------------------
        // Funci�n:    openFileDialog_FileOk
        // Prop�sito:  
        // Fecha:      mi�rcoles, 01 de febrero de 2006, 15:30:02
        //--------------------------------------------------------------------
        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            this.Text = "Hydrium Mobile Editor - " + openFileDialog.FileName;
            m_EditorManager.OpenFile(openFileDialog.OpenFile());  
        }


        //--------------------------------------------------------------------
        // Funci�n:    numericUpDownCursorX_ValueChanged
        // Prop�sito:  
        // Fecha:      viernes, 03 de febrero de 2006, 9:16:10
        //--------------------------------------------------------------------
        private void numericUpDownCursorX_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCursorY.Value = 1;
        }


        //--------------------------------------------------------------------
        // Funci�n:    numericUpDownCursorY_ValueChanged
        // Prop�sito:  
        // Fecha:      viernes, 03 de febrero de 2006, 9:16:25
        //--------------------------------------------------------------------
        private void numericUpDownCursorY_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCursorX.Value = 1;
        }


        //--------------------------------------------------------------------
        // Funci�n:    abrirToolStripMenuItem1_Click
        // Prop�sito:  
        // Fecha:      mi�rcoles, 08 de febrero de 2006, 13:40:14
        //--------------------------------------------------------------------
        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }


        //--------------------------------------------------------------------
        // Funci�n:    guardarComoToolStripMenuItem1_Click
        // Prop�sito:  
        // Fecha:      mi�rcoles, 08 de febrero de 2006, 13:40:06
        //--------------------------------------------------------------------
        private void guardarComoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (m_EditorManager.m_iTeleport % 2 == 1)
            {
                MessageBox.Show("Hay un teletransportador sin pareja.");

                return;
            }

            saveFileDialog.ShowDialog();
        }


        //--------------------------------------------------------------------
        // Funci�n:    salirToolStripMenuItem1_Click
        // Prop�sito:  
        // Fecha:      mi�rcoles, 08 de febrero de 2006, 13:39:57
        //--------------------------------------------------------------------
        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //--------------------------------------------------------------------
        // Funci�n:    nuevoToolStripMenuItem1_Click
        // Prop�sito:  
        // Fecha:      jueves, 09 de febrero de 2006, 18:01:06
        //--------------------------------------------------------------------
        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Text = "Hydrium Mobile Editor";
            m_EditorManager.ResetScene();
            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    checkBoxFondo_CheckedChanged
        // Prop�sito:  
        // Fecha:      lunes, 23 de octubre de 2006, 18:31:07
        //--------------------------------------------------------------------
        private void checkBoxFondo_CheckedChanged(object sender, EventArgs e)
        {
            m_EditorManager.BackgroundActivate = checkBoxFondo.Checked;
            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    comboBoxFondo_SelectedIndexChanged
        // Prop�sito:  
        // Fecha:      lunes, 23 de octubre de 2006, 18:31:31
        //--------------------------------------------------------------------
        private void comboBoxFondo_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_EditorManager.Background = comboBoxFondo.SelectedIndex;
            pictureBoxRendering.Invalidate();
        }


        //--------------------------------------------------------------------
        // Funci�n:    exportarParaOzoneToolStripMenuItem_Click
        // Creador:    Nacho (AMD)
        // Fecha:      Friday  05/10/2007  17:16:37
        //--------------------------------------------------------------------
        private void exportarParaOzoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_EditorManager.m_iTeleport % 2 == 1)
            {
                MessageBox.Show("Hay un teletransportador sin pareja.");

                return;
            }

            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {            
            m_EditorManager.ExportFile(saveFileDialog1.OpenFile());
        }       
    }
}