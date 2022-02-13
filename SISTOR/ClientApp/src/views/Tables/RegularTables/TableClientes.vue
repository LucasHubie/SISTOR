<template>
  <div>
    <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
      <b-card-header class="border-0">
        <h3 class="mb-0 float-left">Clientes</h3>

        <base-button type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Filtrar</span>
        </base-button>

        <base-button v-b-modal.modal-1 type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v:onclick="">
          <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Adicionar</span>
        </base-button>

      </b-card-header>

      <el-table class="table-responsive table"
                header-row-class-name="thead-light"
                :data="clientes">


        <el-table-column label="Nome"
                         min-width="250px"
                         prop="name">
          <template v-slot="{row}">
            <b-media no-body class="align-items-center">
              <b-media-body>
                <span class="font-weight-600 name mb-0 text-sm">{{row.pessoa.nome}}</span>
              </b-media-body>
            </b-media>
          </template>
        </el-table-column>

        <el-table-column label="Nome Fantasia"
                         prop="pessoa.nomeFantasia"
                         min-width="250px">
        </el-table-column>
        <el-table-column label="E-mail"
                         prop="pessoa.email"
                         min-width="250px">
        </el-table-column>
        <el-table-column label="Telefone"
                         prop="pessoa.telefone"
                         min-width="150px">
        </el-table-column>
        <el-table-column label="Ações"
                         min-width="200px"
                         prop="">
          <template v-slot="{row}">

            <base-button v-on:click="getClienteById(row.id)" v-b-modal.modal-3 size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon></base-button>
            <base-button v-on:click="getClienteById(row.id)" v-b-modal.modal-2 size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
            <base-button v-on:click="deleteClienteConfirmed(row.id)" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>

          </template>
        </el-table-column>

      </el-table>

      <b-card-footer class="py-4 d-flex justify-content-end">
        <base-pagination v-model="currentPage" :per-page="10" :total="50"></base-pagination>
      </b-card-footer>
      <!--Modal inclusão-->
      <b-modal id="modal-1" title="Incluir Cliente" size="xl" @show="resetModal"
               @hidden="resetModal"
               @ok="handleOk">

        <form ref="form" @submit.stop.prevent="handleSubmit">

          <b-form-group>

            <h6 class="heading-small text-muted mb-4">Informações do Cliente</h6>

            <div class="">
              <b-form-group>
                <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="F">Pessoa Fisica</b-form-radio>
                <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="J">Pessoa Júridica</b-form-radio>

              </b-form-group>
              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="Nome"
                                label-for="name-input"
                                invalid-feedback="Nome é obrigatório"
                                :state="nameState">
                    <b-form-input id="name-input"
                                  placeholder="Nome"
                                  v-model="Cliente.Pessoa.nome"
                                  :state="nameState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="Nome Fantasia"
                                label-for="name-input"
                                invalid-feedback="Nome Fantasia é obrigatório"
                                :state="nameState">
                    <b-form-input id="name-input"
                                  placeholder="Nome Fantasia"
                                  v-model="Cliente.Pessoa.nomeFantasia"
                                  :state="nameState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="CPF"
                                label-for="cpf-input"
                                invalid-feedback="CPF é obrigatório"
                                :state="cpfState">
                    <b-form-input id="cpf-input"
                                  placeholder="000.000.000-00"
                                  v-model="Cliente.Pessoa.cpf"
                                  :state="cpfState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="CNPJ"
                                label-for="cnpj-input"
                                invalid-feedback="CNPJ é obrigatório"
                                :state="cnpjState">
                    <b-form-input id="cnpj-input"
                                  placeholder="000.000.000-00"
                                  v-model="Cliente.Pessoa.cnpj"
                                  :state="cnpjState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="RG"
                                label-for="rg-input"
                                invalid-feedback="RG é obrigatório"
                                :state="rgState">
                    <b-form-input id="rg-input"
                                  placeholder="RG"
                                  v-model="Cliente.Pessoa.rg"
                                  :state="rgState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="Razão Social"
                                label-for="razaosocial-input"
                                invalid-feedback="Razão Social é obrigatório"
                                :state="razaosocialState">
                    <b-form-input id="razaosocial-input"
                                  placeholder="Razão Social"
                                  v-model="Cliente.Pessoa.razaoSocial"
                                  :state="razaosocialState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6">

                  <b-form-group label="E-mail"
                                label-for="email-input"
                                invalid-feedback="E-mail é obrigatório"
                                :state="emailState">
                    <b-form-input id="email-input"
                                  placeholder="E-mail"
                                  v-model="Cliente.Pessoa.email"
                                  :state="emailState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Telefone"
                              placeholder="Telefone"
                              v-model="Cliente.Pessoa.telefone">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Celular"
                              placeholder="Celular"
                              v-model="Cliente.Pessoa.celular">
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="6">

                  <b-form-group label="Cidade"
                                label-for="cidade-input"
                                invalid-feedback="Cidade é obrigatória"
                                :state="cidadeState">
                    <b-form-input id="cidade-input"
                                  placeholder="Cidade"
                                  v-model="Cliente.Pessoa.cidade"
                                  :state="cidadeState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="3">
                  <b-form-group label="UF"
                                label-for="uf-input"
                                invalid-feedback="UF é obrigatório"
                                :state="ufState">

                    <b-form-select v-model="Cliente.Pessoa.uf"
                                   :state="ufState"
                                   required
                                   :options="options">

                    </b-form-select>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="CEP"
                                label-for="cep-input"
                                invalid-feedback="CEP é obrigatório"
                                :state="cepState">
                    <b-form-input id="cep-input"
                                  placeholder="CEP"
                                  v-model="Cliente.Pessoa.cep"
                                  :state="cepState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="Endereco"
                                label-for="endereco-input"
                                invalid-feedback="Endereço é obrigatório"
                                :state="enderecoState">
                    <b-form-input id="endereco-input"
                                  placeholder="Endereço"
                                  v-model="Cliente.Pessoa.endereco"
                                  :state="enderecoState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="4">
                  <base-input type="text"
                              label="Número"
                              placeholder="Número"
                              v-model="Cliente.Pessoa.numero">
                  </base-input>
                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Referência"
                              placeholder="Referência"
                              v-model="Cliente.Pessoa.referencia">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Complemento"
                              placeholder="Complemento"
                              v-model="Cliente.Pessoa.complemento">
                  </base-input>
                </b-col>
              </b-row>
              <hr class="my-4">


            </div>


          </b-form-group>

        </form>

      </b-modal>

      <!--Modal update-->
      <b-modal id="modal-2" title="Atualizar Cliente" size="xl" @show="resetModal"
               @hidden="resetModal"
               @ok="handleOKupdate">

        <form ref="form" @submit.stop.prevent="handleSubmitUpdate">

          <b-form-group>

            <h6 class="heading-small text-muted mb-4">Informações do Cliente</h6>

            <div class="">
              <b-form-group>
                <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="F">Pessoa Fisica</b-form-radio>
                <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="J">Pessoa Júridica</b-form-radio>

              </b-form-group>
              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="Nome"
                                label-for="name-input"
                                invalid-feedback="Nome é obrigatório"
                                :state="nameState">
                    <b-form-input id="name-input"
                                  placeholder="Nome"
                                  v-model="Cliente.Pessoa.nome"
                                  :state="nameState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="Nome Fantasia"
                                label-for="name-input"
                                invalid-feedback="Nome Fantasia é obrigatório"
                                :state="nameState">
                    <b-form-input id="name-input"
                                  placeholder="Nome Fantasia"
                                  v-model="Cliente.Pessoa.nomeFantasia"
                                  :state="nameState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="CPF"
                                label-for="cpf-input"
                                invalid-feedback="CPF é obrigatório"
                                :state="cpfState">
                    <b-form-input id="cpf-input"
                                  placeholder="000.000.000-00"
                                  v-model="Cliente.Pessoa.cpf"
                                  :state="cpfState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="CNPJ"
                                label-for="cnpj-input"
                                invalid-feedback="CNPJ é obrigatório"
                                :state="cnpjState">
                    <b-form-input id="cnpj-input"
                                  placeholder="000.000.000-00"
                                  v-model="Cliente.Pessoa.cnpj"
                                  :state="cnpjState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="RG"
                                label-for="rg-input"
                                invalid-feedback="RG é obrigatório"
                                :state="rgState">
                    <b-form-input id="rg-input"
                                  placeholder="RG"
                                  v-model="Cliente.Pessoa.rg"
                                  :state="rgState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="Razão Social"
                                label-for="razaosocial-input"
                                invalid-feedback="Razão Social é obrigatório"
                                :state="razaosocialState">
                    <b-form-input id="razaosocial-input"
                                  placeholder="Razão Social"
                                  v-model="Cliente.Pessoa.razaoSocial"
                                  :state="razaosocialState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6">

                  <b-form-group label="E-mail"
                                label-for="email-input"
                                invalid-feedback="E-mail é obrigatório"
                                :state="emailState">
                    <b-form-input id="email-input"
                                  placeholder="E-mail"
                                  v-model="Cliente.Pessoa.email"
                                  :state="emailState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Telefone"
                              placeholder="Telefone"
                              v-model="Cliente.Pessoa.telefone">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Celular"
                              placeholder="Celular"
                              v-model="Cliente.Pessoa.celular">
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="6">

                  <b-form-group label="Cidade"
                                label-for="cidade-input"
                                invalid-feedback="Cidade é obrigatória"
                                :state="cidadeState">
                    <b-form-input id="cidade-input"
                                  placeholder="Cidade"
                                  v-model="Cliente.Pessoa.cidade"
                                  :state="cidadeState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="3">
                  <b-form-group label="UF"
                                label-for="uf-input"
                                invalid-feedback="UF é obrigatório"
                                :state="ufState">

                    <b-form-select v-model="Cliente.Pessoa.uf"
                                   :state="ufState"
                                   required
                                   :options="options">

                    </b-form-select>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="CEP"
                                label-for="cep-input"
                                invalid-feedback="CEP é obrigatório"
                                :state="cepState">
                    <b-form-input id="cep-input"
                                  placeholder="CEP"
                                  v-model="Cliente.Pessoa.cep"
                                  :state="cepState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="Endereco"
                                label-for="endereco-input"
                                invalid-feedback="Endereço é obrigatório"
                                :state="enderecoState">
                    <b-form-input id="endereco-input"
                                  placeholder="Endereço"
                                  v-model="Cliente.Pessoa.endereco"
                                  :state="enderecoState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="4">
                  <base-input type="text"
                              label="Número"
                              placeholder="Número"
                              v-model="Cliente.Pessoa.numero">
                  </base-input>
                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Referência"
                              placeholder="Referência"
                              v-model="Cliente.Pessoa.referencia">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Complemento"
                              placeholder="Complemento"
                              v-model="Cliente.Pessoa.complemento">
                  </base-input>
                </b-col>
              </b-row>
              <hr class="my-4">


            </div>


          </b-form-group>

        </form>

      </b-modal>

      <!--Modal detail-->
      <b-modal id="modal-3" title="Detalhes do Cliente" size="xl" @show="resetModal"
               @hidden="resetModal"
               @ok="handleOKdetail">

        <form ref="form" @submit.stop.prevent="handleSubmitUpdate">

          <b-form-group>

            <h6 class="heading-small text-muted mb-4">Informações do Cliente</h6>

            <div class="">
              <b-form-group>
                <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="F">Pessoa Fisica</b-form-radio>
                <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="J">Pessoa Júridica</b-form-radio>

              </b-form-group>

              <!--<b-col lg="6">

                <b-form-group label="Tipo Pessoa"
                              label-for="pessoatipo-input"
                              :state="tipopessoaState">
                  <b-form-input id="pessoatipo-input"
                                placeholder="Tipo de pessoa"
                                v-model="Cliente.Pessoa.tipoPessoa"
                                :state="tipopessoaState"
                                ></b-form-input>
                </b-form-group>

              </b-col>-->

              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="Nome"
                                label-for="name-input"
                                :state="nameState">
                    <b-form-input id="name-input"
                                  placeholder="Nome"
                                  v-model="Cliente.Pessoa.nome"
                                  :state="nameState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="Nome Fantasia"
                                label-for="name-input"
                                invalid-feedback="Nome Fantasia é obrigatório"
                                :state="nameState">
                    <b-form-input id="name-input"
                                  placeholder="Nome Fantasia"
                                  v-model="Cliente.Pessoa.nomeFantasia"
                                  :state="nameState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="CPF"
                                label-for="cpf-input"
                                invalid-feedback="CPF é obrigatório"
                                :state="cpfState">
                    <b-form-input id="cpf-input"
                                  placeholder="000.000.000-00"
                                  v-model="Cliente.Pessoa.cpf"
                                  :state="cpfState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="CNPJ"
                                label-for="cnpj-input"
                                invalid-feedback="CNPJ é obrigatório"
                                :state="cnpjState">
                    <b-form-input id="cnpj-input"
                                  placeholder="000.000.000-00"
                                  v-model="Cliente.Pessoa.cnpj"
                                  :state="cnpjState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6" v-if="selected == 'F'">

                  <b-form-group label="RG"
                                label-for="rg-input"
                                invalid-feedback="RG é obrigatório"
                                :state="rgState">
                    <b-form-input id="rg-input"
                                  placeholder="RG"
                                  v-model="Cliente.Pessoa.rg"
                                  :state="rgState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6" v-if="selected == 'J'">

                  <b-form-group label="Razão Social"
                                label-for="razaosocial-input"
                                invalid-feedback="Razão Social é obrigatório"
                                :state="razaosocialState">
                    <b-form-input id="razaosocial-input"
                                  placeholder="Razão Social"
                                  v-model="Cliente.Pessoa.razaoSocial"
                                  :state="razaosocialState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="6">

                  <b-form-group label="E-mail"
                                label-for="email-input"
                                invalid-feedback="E-mail é obrigatório"
                                :state="emailState">
                    <b-form-input id="email-input"
                                  placeholder="E-mail"
                                  v-model="Cliente.Pessoa.email"
                                  :state="emailState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Telefone"
                              placeholder="Telefone"
                              v-model="Cliente.Pessoa.telefone">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Celular"
                              placeholder="Celular"
                              v-model="Cliente.Pessoa.celular">
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="6">

                  <b-form-group label="Cidade"
                                label-for="cidade-input"
                                invalid-feedback="Cidade é obrigatória"
                                :state="cidadeState">
                    <b-form-input id="cidade-input"
                                  placeholder="Cidade"
                                  v-model="Cliente.Pessoa.cidade"
                                  :state="cidadeState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
                <b-col lg="3">
                  <b-form-group label="UF"
                                label-for="uf-input"
                                invalid-feedback="UF é obrigatório"
                                :state="ufState">

                    <b-form-input v-model="Cliente.Pessoa.uf"
                                  :state="ufState"
                                  required
                                  :options="options">

                    </b-form-input>
                  </b-form-group>
                </b-col>
              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="CEP"
                                label-for="cep-input"
                                invalid-feedback="CEP é obrigatório"
                                :state="cepState">
                    <b-form-input id="cep-input"
                                  placeholder="CEP"
                                  v-model="Cliente.Pessoa.cep"
                                  :state="cepState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

              </b-row>
              <b-row>

                <b-col lg="6">

                  <b-form-group label="Endereco"
                                label-for="endereco-input"
                                invalid-feedback="Endereço é obrigatório"
                                :state="enderecoState">
                    <b-form-input id="endereco-input"
                                  placeholder="Endereço"
                                  v-model="Cliente.Pessoa.endereco"
                                  :state="enderecoState"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>

                <b-col lg="4">
                  <base-input type="text"
                              label="Número"
                              placeholder="Número"
                              v-model="Cliente.Pessoa.numero">
                  </base-input>
                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Referência"
                              placeholder="Referência"
                              v-model="Cliente.Pessoa.referencia">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Complemento"
                              placeholder="Complemento"
                              v-model="Cliente.Pessoa.complemento">
                  </base-input>
                </b-col>
              </b-row>
              <hr class="my-4">


            </div>


          </b-form-group>

        </form>

      </b-modal>
      <!--Modal detail-->

    </b-card>
  </div>


</template>

<script>
  import axios from 'axios'
  import products from '../products'
  import { Table, TableColumn, DropdownMenu, DropdownItem, Dropdown } from 'element-ui'
  export default {
    name: 'table-clientes',
    components: {
      [Table.name]: Table,
      [TableColumn.name]: TableColumn,
      [Dropdown.name]: Dropdown,
      [DropdownItem.name]: DropdownItem,
      [DropdownMenu.name]: DropdownMenu,
    },
    data() {
      return {
        clientes: [],
        products,
        uf: [],
        currentPage: 1,
        selected: 'F',
        nameState: null,
        cidadeState: null,
        emailState: null,
        cpfState: null,
        rgState: null,
        ufState: null,
        cepState: null,
        enderecoState: null,
        cnpjState: null,
        razaosocialState: null,
        Cliente: {
          Pessoa: {
            Nome: "",
            RG: "",
            CPF: "",
            CNPJ: "",
            RazaoSocial: "",
            NomeFantasia: "",
            Telefone: "",
            Celular: "",
            Cidade: "",
            Email: "",
            CEP: "",
            Numero: 0,
            Complemento: "",
            Referencia: "",
            Endereco: "",
            TipoPessoa: 0,
            UF: "",
          }
        },
        orcamento: {
          descricao: 'Motor WEG'
        },
        produto: {

        },
        
        options: [
          { value: "AL", text: "Alagoas" },
          { value: "AC", text: "Acre" },
          { value: "AP", text: "Amapa" },
          { value: "AM", text: "Amazonas" },
          { value: "BA", text: "Bahia" },
          { value: "CE", text: "Ceará" },
          { value: "DF", text: "Distrito Federal" },
          { value: "ES", text: "Espirito Santo" },
          { value: "GO", text: "Goias" },
          { value: "MA", text: "Maranhao" },
          { value: "MT", text: "Mato Grosso" },
          { value: "MS", text: "Mato Grosso do Sul" },
          { value: "MG", text: "Minas Gerais" },
          { value: "PR", text: "Parana" },
          { value: "PB", text: "Paraiba" },
          { value: "PA", text: "Para" },
          { value: "PE", text: "Pernambuco" },
          { value: "RJ", text: "Rio de Janeiro" },
          { value: "RS", text: "Rio Grande do Sul" },
          { value: "RR", text: "Roraima" },
          { value: "RO", text: "Rondonia" },
          { value: "SP", text: "São Paulo" },
          { value: "SC", text: "Santa Catarina" },
          { value: "SE", text: "Sergipe" },
          { value: "TO", text: "Tocantins" }
        ],
        showNovoProduto: false
      };
    },
    methods: {
      checkFormValidity() {
        const valid = this.$refs.form.checkValidity()
        this.nameState = valid
        this.cidadeState = valid
        this.emailState = valid
        this.cpfState = valid
        this.rgState = valid
        this.ufState = valid
        this.cepState = valid
        this.enderecoState = valid
        this.cnpjState = valid
        this.razaosocialState = valid
        return valid
      },
      
      resetModal() {
        this.Cliente.Pessoa.Nome = ''
        this.Cliente.Pessoa.CPF = ''
        this.Cliente.Pessoa.Celular = ''
        this.Cliente.Pessoa.CEP = ''
        this.Cliente.Pessoa.CNPJ = ''
        this.Cliente.Pessoa.Complemento = ''
        this.Cliente.Pessoa.Email = ''
        this.Cliente.Pessoa.Endereco = ''
        this.Cliente.Pessoa.NomeFantasia = ''
        this.Cliente.Pessoa.RazaoSocial = ''
        this.Cliente.Pessoa.Numero = 0
        this.Cliente.Pessoa.UF = ''
        this.Cliente.Pessoa.Referencia = ''
        this.Cliente.Pessoa.RG = ''
        this.Cliente.Pessoa.Telefone = ''
        this.Cliente.Pessoa.Cidade = ''
        this.nameState = null
        this.cidadeState = null
        this.emailState = null
        this.cpfState = null
        this.rgState = null
        this.ufState = null
        this.cepState = null
        this.enderecoState = null
        this.cnpjState = null
        this.razaosocialState = null
      },
      tipoPessoa() {
        if (this.selected == 'F') {
          this.Cliente.Pessoa.TipoPessoa = 1
        } if (this.selected == 'J') {
          this.Cliente.Pessoa.TipoPessoa = 2
        }
      },
      handleOk(bvModalEvt) {
        // Prevent modal from closing
        bvModalEvt.preventDefault()
        // chama função tipoPessoa
        this.tipoPessoa()
        // Trigger submit handler
        this.handleSubmit()
      },
      handleOKupdate(bvModalEvt) {
        // Prevent modal from closing
        bvModalEvt.preventDefault()
        // chama função tipoPessoa
        this.tipoPessoa()
        // Trigger submit handler
        this.handleSubmitUpdate()
      },
      handleOKdetail(bvModalEvt) {
        // Prevent modal from closing
        bvModalEvt.preventDefault()
        // Trigger submit handler
        this.handleSubmitDetail()
      },
      handleSubmit() {
        // Exit when the form isn't valid
        if (!this.checkFormValidity()) {
          return
        }
        // chama função post incluir cliente
          this.sendForm();
        // Hide the modal manually
        this.$nextTick(() => {
          this.$bvModal.hide('modal-1')
        })
      },
      handleSubmitUpdate() {
        // Exit when the form isn't valid
        if (!this.checkFormValidity()) {
          return
        }
        // chama função post update cliente
        this.updateCliente();
        // Hide the modal manually
        this.$nextTick(() => {
          this.$bvModal.hide('modal-2')
        })
      },
      handleSubmitDetail() {
        this.$nextTick(() => {
          this.$bvModal.hide('modal-3')
        })
      },

      getClientes() {
        axios.get("https://localhost:44376/Cliente/Index", {
        }).then(response => {
          console.log(response.data)
          this.clientes = response.data
        })
          .catch(function (error) {
            alert(error);
          });
      },

      getClienteById(id) {
        axios.get("https://localhost:44376/Cliente/GetClienteById", {
          params: { "id": id }
        }).then(response => {
          console.log(response.data)
          this.Cliente = response.data.cliente
          this.Cliente.Pessoa = response.data.cliente.pessoa
          console.log(this.Cliente)
        })
          .catch(function (error) {
            alert(error);
          });
      },

      sendForm() {
       
        axios.post("https://localhost:44376/Cliente/Create", {
          Pessoa: this.Cliente.Pessoa
        }).then(response => {
          if (response.data.sucess = true) {
            console.log(response.data)
            alert(response.data.description)
            //window.location.href = "#/funcionarios"
            this.$bvModal.hide("modal-1")
            this.getClientes()
          }
          else {
            alert(response.data.description)
          }
        })
          .catch(function (error) {
            alert(error);
          });
        // this will be called only after form is valid. You can do api call here to login
      },

      deleteClienteConfirmed(id) {
        this.$bvModal.msgBoxConfirm('Por favor confirme a exclusão.', {
          title: 'Deseja excluir este cliente?',
          size: 'sm',
          buttonSize: 'sm',
          okVariant: 'danger',
          okTitle: 'Sim',
          cancelTitle: 'Não',
          footerClass: 'p-2',
          hideHeaderClose: false,
          centered: true
        })
          .then(value => {
            this.boxTwo = value
            this.deleteCliente(id)
            console.log(value)
          })
          .catch(err => {
            // An error occurred
          })
      },

      deleteCliente(id) {
      axios.get("https://localhost:44376/Cliente/Excluir", {
        params: { "id": id }
      }).then(response => {
        if (response.data.sucess = true) {
          console.log(response.data)
          alert(response.data.description)
          this.getClientes()
        }
        else {
          alert(response.data.description)
        }
      })
        .catch(function (error) {
          alert(error);
        });
    },

      updateCliente() {
        axios.post("https://localhost:44376/Cliente/Update", {
          Pessoa: this.Cliente.Pessoa,
          id: this.Cliente.id
        }).then(response => {
          if (response.data.sucess = true) {
            console.log(response.data)
            alert(response.data.description)
            this.$bvModal.hide("modal-2")
            this.getClientes()
          }
          else {
            alert(response.data.description)
          }
        })
          .catch(function (error) {
            alert(error);
          });
      }
      
    },

   

    mounted() {
      this.getClientes();
    }
  }
</script>

<style>
  input[type], input[type="*"] {
    color: black
  }

  .form-control {
    color: black !important;
  }
</style>
