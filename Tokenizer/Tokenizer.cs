﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{
    internal class Tokenizer
    {
        //percorrer a linha armazenando cada caractere ou tolken
        private int index;
        private bool temAspas = false;
        private string caractere, linha, tolken;
        private List<string> tolkens = new List<string>();
        private string[] separadores;

        public void Tokenizar(string linha, string[] separadores )
        {
            this.index = 0;
            this.linha = linha;
            this.separadores = separadores;
        }

        private void finalizarTolken()
        {
            if (this.tolken == null)
            {
                this.index++;
            }
            else
            {
                this.tolkens.Add(this.tolken);
                this.tolken = null;
                this.index++;
            }
        }
        private bool identificar()
        {
            if (this.index == linha.Length)
            {
                finalizarTolken();
                return false;
            }

            this.caractere = this.linha[this.index].ToString();

            if (this.caractere == " ")
            {
                finalizarTolken();
                this.caractere = null;
                return true;
            }
            else if (this.separadores.Contains(this.caractere))
            {
                finalizarTolken();   
                this.tolkens.Add(this.caractere);
                this.caractere = null;
                return true;
            } 
            //implementação das aspas como string
            else if (this.caractere == "\'" && this.temAspas == false) 
            {
                finalizarTolken();
                this.temAspas = true;
                this.tolken = this.tolken + this.caractere;
                this.caractere = null;
                return true;
            }
            else if (this.caractere == "\'" && this.temAspas == true)
            {
                this.temAspas = false;
                this.tolken = this.tolken + this.caractere;
                this.caractere = null;
                finalizarTolken();
                return true;
            }
            //fim da implementação
            else
            {
                this.tolken = this.tolken+this.caractere;
                this.index++;
                return true;
            }
        }

        private void acharTolkens()
        {
            bool temTolken = true;
            while (temTolken)
            {
                temTolken = identificar();
            }
        }
        public string[] Tolkens()
        {
            acharTolkens();
            string[] tolkensF = this.tolkens.ToArray();
            return tolkensF;
        }
    }
}
