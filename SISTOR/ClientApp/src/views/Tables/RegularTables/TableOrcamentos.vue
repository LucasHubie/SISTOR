<template>
  <div>
    <b-alert :show="dismissCountDown" dismissible @dismissed="dismissCountDown=0" @dismiss-count-down="countDownChanged" :variant="variant">
      {{txtAlert}}
    </b-alert>
    <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
      <b-card-header class="border-0">
        <h3 class="mb-0 float-left">Orçamentos</h3>
        <base-button v-on:click="showFiltrar = !showFiltrar" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Filtrar</span>
        </base-button>
        <base-button v-b-modal.modal-1 v-on:click="tpOperacao = 'Adicionar'; orcamento = { situacao: 3}, cliente = { pessoa: {}}; clientes = []; buscaproduto=''; lstprodutos = []; nomeclientebusca = ''; showBuscarCliente = true" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
          <span class="btn-inner--text">Adicionar</span>
        </base-button>
        <!--<base-button v-on:click="funcDesenv()" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
          <b-icon icon="card-list" font-scale="1"></b-icon>
          <span class="btn-inner--text">Gerar Relatórios</span>
        </base-button>-->

      </b-card-header>
      <div v-if="showFiltrar" class="modal-body">
        <b-row>
          <b-col lg="3">
            <base-input type="text"
                        label="Tag Identificação/Cliente"
                        placeholder="Nome"
                        v-model="filtro.nome">
            </base-input>
          </b-col>
          <b-col lg="1" v-if="selected == 'F'">
            <base-button type="success" class="float-right" style=" margin: 0; position: absolute; top: 50%; -ms-transform: translateY(-50%); transform: translateY(-50%);" v-on:click="getOrcamentos(1,10)">
              <span class="btn-inner--text">Filtrar</span>
            </base-button>
          </b-col>

        </b-row>
      </div>
      <el-table class="table-responsive table"
                header-row-class-name="thead-light"
                :data="orcamentos">
        <el-table-column label="Tag Identificação"
                         min-width="150px"
                         prop="tagidentificacao">
          <template v-slot="{row}">
            <b-media no-body class="align-items-center">
              <b-media-body>
                <span class="font-weight-600 name mb-0 text-sm">{{row.tagIdentificacao}}</span>
              </b-media-body>
            </b-media>
          </template>
        </el-table-column>
        <el-table-column label="Cliente"
                         prop="cliente.pessoa.nome"
                         min-width="200px">
        </el-table-column>

        <el-table-column label="Status"
                         min-width="170px"
                         prop="status">
          <template v-slot="{row}">
            <badge class="badge-dot mr-4" type="">
              <span class="status text-success">{{ getTextoSituacao(row.situacao) }}</span>
            </badge>
          </template>
        </el-table-column>
        <el-table-column label="Ações"
                         min-width="170px"
                         prop="">
          <template v-slot="{row}">
            <el-dropdown trigger="click" class="dropdown">
              <base-button size="sm" type="default" style="background-color: rgb(58 99 167); margin-right: .5rem;"><b-icon icon="three-dots" font-scale="1"></b-icon></base-button>
              <el-dropdown-menu class="dropdown-menu dropdown-menu-arrow show" slot="dropdown">
                <b-dropdown-item v-on:click="showModalOS(row.id, row.cliente.id, row.tagIdentificacao)">Gerar Ordem de Serviço</b-dropdown-item>
                <b-dropdown-item v-on:click="funcDesenv()">Alterar Situação</b-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
            <base-button v-on:click="funcDesenv()" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="eye-fill" font-scale="1"></b-icon> </base-button>
            <base-button v-on:click="showModal('Editar', row.id)" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="pencil-fill" font-scale="1"></b-icon></base-button>
            <base-button v-on:click="deleteOrcamento('Excluir', row.id)" size="sm" type="default" style="background-color: rgb(58 99 167) "><b-icon icon="trash-fill" font-scale="1"></b-icon></base-button>
          </template>
        </el-table-column>
        <template slot="empty">
          Sem Registros
        </template>
      </el-table>

      <b-card-footer class="py-4 d-flex justify-content-end">
        <base-pagination v-model="currentPage" :per-page="10" :total="qntdRegistros" @change="change"></base-pagination>
      </b-card-footer>
      <b-modal id="modal-1" :title="tpOperacao + ' Orçamento'" size="xl">
        <validation-observer v-slot="{handleSubmit}" ref="formValidatorOrcamento">
          <b-form @submit.prevent="handleSubmit(sendForm)">
            <h6 class="heading-small text-muted mb-4">Informações do Cliente</h6>
            <div class="">
              <b-row>
                <b-col lg="6" v-if="showBuscarCliente">
                  <small>
                    Informe o Nome/CPF/CPNJ para encontrar o cliente
                  </small>
                </b-col>
                <b-col lg="12" v-if="!showBuscarCliente">
                  <base-button v-on:click="showBuscarCliente = true; cliente = { pessoa: {}};nomeclientebusca = ''" size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
                    <span class="btn-inner--text">Trocar Cliente</span>
                  </base-button>
                </b-col>
              </b-row>
              <b-row v-if="showBuscarCliente">
                <b-col lg="4">
                  <base-input type="text"
                              placeholder="Nome/CPF/CPNJ"
                              v-model="nomeclientebusca">
                  </base-input>
                </b-col>
              </b-row>
              <div v-if="!showBuscarCliente">
                <b-row v-if="cliente.pessoa.tipoPessoa == 1">
                  <b-col lg="4">
                    <base-input type="text"
                                label="Nome"
                                name="Nome"
                                disabled
                                placeholder="Nome"
                                v-model="cliente.pessoa.nome">
                    </base-input>
                  </b-col>
                  <b-col lg="3">
                    <base-input type="text"
                                label="CPF"
                                name="CPF"
                                disabled
                                placeholder="000.000.000-00"
                                v-mask="'###.###.###-##'"
                                v-model="cliente.pessoa.cpf"
                                required>
                    </base-input>
                  </b-col>

                </b-row>
                <b-row v-if="cliente.pessoa.tipoPessoa == 2">
                  <b-col lg="3">
                    <base-input type="text"
                                label="Nome Fantasia"
                                name="Nome Fantasia"
                                disabled
                                placeholder="Nome Fantasia"
                                v-model="cliente.pessoa.nome"
                                required>
                    </base-input>
                  </b-col>
                  <b-col lg="3">
                    <base-input type="text"
                                label="CNPJ"
                                name="CNPJ"
                                disabled
                                v-mask="'##.###.###/####-##'"
                                placeholder="00.000.000/0000-00"
                                v-model="cliente.pessoa.cnpj"
                                required>
                    </base-input>
                  </b-col>
                </b-row>
              </div>
              <b-row>
                <b-col lg="6">
                  <small>
                    {{msgBuscando}}
                  </small>
                </b-col>
              </b-row>
              <b-row>
                <div v-if="clientes.length > 0" class="container">
                  <h4>Selecione o cliente abaixo</h4>
                  <table class="table table-striped table-bordered">
                    <tbody>
                      <tr v-for="clienteloop in clientes" style=" background-color: white;" :key="clienteloop.id">
                        <td style="vertical-align: middle; ">{{clienteloop.pessoa.nome}}</td>
                        <td style="vertical-align: middle; ">
                          <div v-if="clienteloop.pessoa.tipoPessoa == 1">   {{cpf(clienteloop.pessoa.cpf)}}</div>
                          <div v-if="clienteloop.pessoa.tipoPessoa == 2"> {{cnpj(clienteloop.pessoa.cnpj)}}</div>
                        </td>
                        <td>
                          <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="cliente = clienteloop; clientes = []; showBuscarCliente = false">
                            <span class="btn-inner--text">Selecionar</span>
                          </base-button>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </div>

              </b-row>
              <b-row v-if="showNovoCliente">
                <b-col lg="6">
                  <small>

                  </small>
                </b-col>
                <b-col lg="6">
                  <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="showModalNovoCliente">
                    <b-icon icon="person-plus-fill" font-scale="1"></b-icon>
                    <span class="btn-inner--text">Novo Cliente</span>
                  </base-button>
                </b-col>
              </b-row>
            </div>
            <hr class="my-4">
            <h6 class="heading-small text-muted mb-4">Descrição Orçamento</h6>
            <b-row>
              <b-col lg="3">
                <base-input type="text"
                            label="Tag Identificação"
                            placeholder="Tag Identificação"
                            name="Tag Identificação"
                            v-model="orcamento.tagIdentificacao"
                            required>
                </base-input>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="9">
                <base-input type="text"
                            label="Descrição do Orçamento"
                            placeholder="Descrição"
                            name="Descrição"
                            v-model="orcamento.descricao"
                            required>
                </base-input>
              </b-col>
            </b-row>
            <hr class="my-4">

            <!-- Address -->
            <!-- Address -->
            <h6 class="heading-small text-muted mb-4">Produtos</h6>

            <div class="">
              <!--<b-row>
      <b-col lg="12">
        <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px; margin-bottom: 10px;" v-on:click="showNovoProduto = !showNovoProduto">
          <b-icon icon="clipboard-plus" font-scale="1"></b-icon>
          <span class="btn-inner--text">Adicionar Produto</span>
        </base-button>
      </b-col>
    </b-row>-->
              <b-row v-if="true">
                <b-col lg="3">
                  <base-input type="text"
                              label="Informe o Código/Descrição"
                              placeholder="Código/Descrição"
                              v-model="buscaproduto">
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <div v-if="produtos.length > 0" class="container">
                  <table class="table table-striped table-bordered">
                    <thead>
                      <tr>
                        <th>Código</th>
                        <th>Descrição</th>
                        <th>Valor</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="produtoloop in produtos" style=" background-color: white;" :key="produtoloop.id">
                        <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.codigo}}</td>
                        <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.descricao}}</td>
                        <td style="vertical-align: middle; " class="tdpading05">R$ {{produtoloop.valor}}</td>
                        <td style="text-align: right; width: 10%" class="tdpading05">
                          <input type="number"
                                 label="Quantidade"
                                 placeholder="Quantidade"
                                 v-model="produtoloop.qntd">
                        </td>
                        <td style="width:10%" class="tdpading05">
                          <base-button :disabled="produtoloop.qntd == null || produtoloop.qntd == '' || produtoloop.qntd == 0" size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="addProduto(produtoloop)">
                            <span class="btn-inner--text">Adicionar</span>
                          </base-button>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </div>
                <h5 v-if="buscaproduto != '' && produtos.length == 0">Nenhum produto encontrado</h5>
              </b-row>
              <b-row>
                <div v-if="lstprodutos.length > 0" class="container">
                  <h4>Itens do Orçamento</h4>
                  <table class="table">
                    <thead>
                      <tr>
                        <th>Código</th>
                        <th>Descrição</th>
                        <th>Valor Unitário</th>
                        <th>Qntd</th>
                        <th>Valor Total</th>
                        <th></th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="(produtoloop, index) in lstprodutos" style=" background-color: white;" :key="produtoloop.id">
                        <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.codigo}}</td>
                        <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.descricao}}</td>
                        <td style="vertical-align: middle; " class="tdpading05">R$ {{produtoloop.valorItem}}</td>
                        <td style="vertical-align: middle; " class="tdpading05">{{produtoloop.quantidade}}</td>
                        <td style="vertical-align: middle; " class="tdpading05">R$ {{produtoloop.valorItem * produtoloop.quantidade}}</td>
                        <td style="width:10%" class="tdpading05">
                          <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="removeProduto(index)">
                            <span class="btn-inner--text">Remover</span>
                          </base-button>
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </b-row>

            </div>

            <hr class="my-4">
            <!-- Description -->
            <h6 class="heading-small text-muted mb-4">Finalização Orçamento</h6>
            <b-row>
              <b-col lg="3">
                <base-input type="number"
                            label="Valor Mão de Obra"
                            placeholder="Valor Mão de Obra"
                            name="Valor Mão de Obra"
                            v-model="orcamento.maoDeObra"
                            required>
                </base-input>
              </b-col>
              <b-col lg="3">
                <label>
                  Situação
                </label>
                <b-form-select v-model="orcamento.situacao" name="Situação" value="3" :options="options" style="color: black;" required></b-form-select>
              </b-col>
            </b-row>
            <b-row>
              <b-col lg="4">
                Valor Total Orçamento: R$ {{ Sum  }}
              </b-col>
            </b-row>
            <b-alert :show="dismissCountDown" dismissible @dismissed="dismissCountDown=0" @dismiss-count-down="countDownChanged" :variant="variant">
              {{txtAlert}}
            </b-alert>
          </b-form>
        </validation-observer>
            <template #modal-footer="{ cancel }">
              <b-row>
                <b-col lg="12">
                  <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="sendForm()">
                    <span class="btn-inner--text">Confirmar</span>
                  </base-button>
                  <base-button type="secondary" class="float-right" style="margin-right: 10px;" @click="cancel()">
                    <span class="btn-inner--text">Cancelar</span>
                  </base-button>
                </b-col>
              </b-row>
            </template>
      </b-modal>
      <b-modal id="modal-multi-2" title="Incluir Cliente" size="lg">
        <validation-observer v-slot="{handleSubmit}" ref="formValidator">
          <b-form @submit.prevent="handleSubmit(sendFormCliente)">
            <h6 class="heading-small text-muted mb-4">Informações do Cliente</h6>
            <div class="">
              <b-form-group>
                <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="F">Pessoa Fisica</b-form-radio>
                <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="J">Pessoa Júridica</b-form-radio>
              </b-form-group>
              <b-row>
                <b-col lg="4" v-if="selected == 'F'">
                  <base-input type="text"
                              label="Nome"
                              name="Nome"
                              :rules="{required: true}"
                              placeholder="Nome"
                              v-model="cliente.pessoa.nome">
                  </base-input>
                </b-col>
                <b-col lg="3" v-if="selected == 'J'">
                  <base-input type="text"
                              label="Nome Fantasia"
                              name="Nome Fantasia"
                              placeholder="Nome Fantasia"
                              v-model="cliente.pessoa.nome"
                              required>
                  </base-input>
                </b-col>
                <b-col lg="3" v-if="selected == 'F'">
                  <base-input type="text"
                              label="CPF"
                              name="CPF"
                              placeholder="000.000.000-00"
                              v-mask="'###.###.###-##'"
                              v-model="cliente.pessoa.cpf"
                              required>
                  </base-input>
                </b-col>
                <b-col lg="3" v-if="selected == 'J'">
                  <base-input type="text"
                              label="CNPJ"
                              name="CNPJ"
                              v-mask="'##.###.###/####-##'"
                              placeholder="00.000.000/0000-00"
                              v-model="cliente.pessoa.cnpj"
                              required>
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="6" v-if="selected == 'F'">
                  <base-input type="text"
                              label="RG"
                              name="RG"
                              placeholder="RG"
                              v-model="cliente.pessoa.rg"
                              required>
                  </base-input>
                </b-col>
                <b-col lg="6" v-if="selected == 'J'">
                  <base-input type="text"
                              label="Razão Social"
                              name="Razão Social"
                              placeholder="Razão Social"
                              v-model="cliente.pessoa.razaoSocial"
                              required>
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="E-mail"
                              name="E-mail"
                              placeholder="E-mail"
                              v-model="cliente.pessoa.email"
                              required>
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Telefone"
                              name="Telefone"
                              placeholder="Telefone"
                              v-model="cliente.pessoa.telefone"
                              required>
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Telefone Celular"
                              name="Telefone Celular"
                              placeholder="Telefone Celular"
                              v-model="cliente.pessoa.celular"
                              required>
                  </base-input>
                </b-col>
                <b-col lg="6">

                  <b-form-group label="Cidade*"
                                label-for="cidade-input"
                                name="Cidade"
                                invalid-feedback="Cidade é obrigatória">
                    <b-form-input id="cidade-input"
                                  placeholder="Cidade"
                                  v-model="cliente.pessoa.cidade"
                                  required></b-form-input>
                  </b-form-group>

                </b-col>
              </b-row>
              <b-row>
                <b-col lg="12">
                  <base-input type="text"
                              label="Endereço"
                              name="Endereço"
                              placeholder="Endereço"
                              v-model="cliente.pessoa.endereco"
                              required>
                  </base-input>
                </b-col>
              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="CEP"
                              name="CEP"
                              placeholder="CEP"
                              v-model="cliente.pessoa.cep"
                              required>
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Número"
                              name="Número"
                              placeholder="Número"
                              v-model="cliente.pessoa.numero"
                              required>
                  </base-input>
                </b-col>

              </b-row>
              <b-row>
                <b-col lg="6">
                  <base-input type="text"
                              label="Referência"
                              name="Referência"
                              placeholder="Referência"
                              v-model="cliente.pessoa.referencia">
                  </base-input>
                </b-col>
                <b-col lg="6">
                  <base-input type="text"
                              label="Complemento"
                              name="Complemento"
                              placeholder="Complemento"
                              v-model="cliente.pessoa.complemento">
                  </base-input>
                </b-col>
              </b-row>
            </div>
            <b-alert :show="dismissCountDown" dismissible @dismissed="dismissCountDown=0" @dismiss-count-down="countDownChanged" :variant="variant">
              {{txtAlert}}
            </b-alert>
          </b-form>
        </validation-observer>
        <template #modal-footer="{ cancel }">
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="sendFormCliente()">
                <span class="btn-inner--text">Confirmar</span>
              </base-button>
              <base-button type="secondary" class="float-right" style="margin-right: 10px;" @click="cancel()">
                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
        </template>

      </b-modal>
      <b-modal id="modal-2" title="Ordem Serviço" size="xl">
        <b-form @submit.prevent="updateProfile">
          <h6 class="heading-small text-muted mb-4">Informações da Ordem Serviço</h6>
          <div class="">
            <b-row>
              <label>
                Tag Identificação: {{ordemServico.nome}}
              </label>
            </b-row>
          </div>
          <b-row>
            <b-col lg="6">
              <label>
                Funcionário
              </label>
              <b-form-select v-model="ordemServico.idFuncionario" :options="funcionarios"></b-form-select>
            </b-col>
          </b-row>
          <b-row>
            <div class="">
              <b-form-group label="Descrição Adicional" label-class="form-control-label" class="mb-0" label-for="about-form-textaria">
                <!--  <label class="form-control-label">About Me</label> -->
                <b-form-textarea rows="4" v-model="ordemServico.observacoes" id="about-form-textaria" placeholder="Descrição da Ordem de Serviço"></b-form-textarea>
              </b-form-group>
            </div>
          </b-row>
        </b-form>
        <template #modal-footer>
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="sendFormOS()">
                <span class="btn-inner--text">Confirmar</span>
              </base-button>
              <base-button type="secondary" class="float-right" style="margin-right: 10px;">

                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
        </template>
      </b-modal>
      <b-modal id="modal-3" title="Orçamento - Situação" size="xl">
        <b-form @submit.prevent="updateProfile">
          <h6 class="heading-small text-muted mb-4">Alterar</h6>
          <div class="">
            <b-row>
              <b-col lg="6">
                <base-input type="text"
                            label="Nome"
                            placeholder="Nome"
                            v-model="orcamento.desc">
                </base-input>
              </b-col>
              <b-col lg="6">
                <base-input type="text"
                            label="Situação"
                            placeholder="Funcionário"
                            v-model="orcamento.func">
                  <select class="form-control">
                    <option>Aprovado</option>
                    <option>Finalizado</option>
                    <option>Cancelado</option>
                    <option>Em Andamento</option>
                  </select>
                </base-input>
              </b-col>
            </b-row>
          </div>

        </b-form>
        <template #modal-footer>
          <b-row>
            <b-col lg="12">
              <base-button type="success" class="float-right" style="margin-right: 10px;">
                <span class="btn-inner--text">Confirmar</span>
              </base-button>
              <base-button type="secondary" class="float-right" style="margin-right: 10px;">

                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
        </template>
      </b-modal>
    </b-card>
  </div>
</template>
<script>
  import axios from 'axios'
  import { Table, TableColumn, DropdownMenu, DropdownItem, Dropdown } from 'element-ui'
  export default {
    name: 'table-orcamentos',
    components: {
      [Table.name]: Table,
      [TableColumn.name]: TableColumn,
      [Dropdown.name]: Dropdown,
      [DropdownItem.name]: DropdownItem,
      [DropdownMenu.name]: DropdownMenu,
    },
    data() {
      return {
        nomeclientebusca: '',
        buscaproduto: '',
        dismissSecs: 5,
        dismissCountDown: 0,
        variant: "info",
        txtAlert: "",
        qntdRegistros: 0,
        showFiltrar: false,
        filtro: { nome: '' },
        orcamentos: [],
        clientes: [],
        produtos: [],
        options: [
          { value: 1, text: 'Aprovado' },
          { value: 3, text: 'Aguardando Aprovação' }
        ],
        optionsall: [
          { value: 1, text: 'Aprovado' },
          { value: 2, text: 'Negado' },
          { value: 3, text: 'Aguardando Aprovação' },
          { value: 4, text: 'Encerrado' },
          { value: 5, text: 'Cancelado' },
        ],
        currentPage: 1,
        selected: 'F',
        user: {
          username: ''
        },
        cliente: {
          pessoa: {
            nome: "",
            rg: "",
            cpf: "",
            cnpj: "",
            razaosocial: "",
            nomefantasia: "",
            telefone: "",
            celular: "",
            email: "",
            cep: "",
            numero: 0,
            complemento: "",
            referencia: "",
            endereco: "",
            tipopessoa: 0,
          }
        },
        orcamento: {
          descricao: "",
          maodeobra: 0,
          valortotal: 0,
          situacao: 0
        },
        produto: {

        },
        ordemServico: {},
        funcionarios: [],
        lstprodutos: [],
        showBtBuscar: true,
        showNovoCliente: false,
        showNovoProduto: false,
        showBuscarCliente: true,
        msgBuscando: "",
        tpOperacao: "Incluir"
      };
    },
    methods: {
      showAlert(message, variant) {
        // success
        // danger
        // warning
        this.dismissCountDown = this.dismissSecs
        this.variant = variant
        this.txtAlert = message
      },
      countDownChanged(dismissCountDown) {
        this.dismissCountDown = dismissCountDown
      },
      getTextoSituacao: function (situacao) {
        console.log(situacao);
        var retorno = "";
        switch (situacao) {
          case 1:
            retorno = "Aprovado";
            break;
          case 2:
            retorno = "Negado";
            break;
          case 3:
            retorno = "Aguardando";
            break;
          case 4:
            retorno = "Encerrado";
            break;
          case 5:
            retorno = "Cancelado";
            break;
        }
        return retorno;
      },


      getOrcamentos(pageN, pageS) {
        let $this = this;
        var busca = this.filtro.nome
        axios.get("https://localhost:44376/Orcamento/GetOrcamentos", {
          params: { "busca": busca, "pageNumber": pageN, "pageSize": pageS }
        }).then(response => {
          this.orcamentos = response.data.lst;
          this.qntdRegistros = response.data.qntdRegistros;
        })
          .catch(function (error) {
            $this.showAlert("Falha ao Carregar Orcamentos", "danger");
          });
      },

      getFuncionarios(pageN, pageS) {
        let $this = this;
        axios.get("https://localhost:44376/Funcionario/Index", {
          params: { "pageNumber": pageN, "pageSize": pageS }
        }).then(response => {

          console.log('func', response.data);
          for (var i = 0; i < response.data.length; i++) {
            var opt = { value: response.data[i].id, text: response.data[i].pessoa.nome };
            this.funcionarios.push(opt);
          }
          console.log('lstfunc', this.funcionarios);

        })
          .catch(function (error) {
            $this.showAlert("Falha ao Carregar Funcionarios", "danger");
          });
      },
      buscarCliente(busca, pageN, pageS) {
        let $this = this;
        this.msgBuscando = "Aguarde Procurando Cliente..."
        axios.get("https://localhost:44376/Cliente/BuscarCliente", {
          params: { "busca": busca, "pageNumber": pageN, "pageSize": pageS }
        }).then(response => {
            this.clientes = response.data.lst;
            $this.msgBuscando = "";
            this.showNovoCliente = false;
            if (this.clientes.length == 0) {
              $this.msgBuscando = "Cliente não encontrado";
              this.showNovoCliente = true;
            }
        })
          .catch(function (error) {
            $this.msgBuscando = "Falha ao procurar Cliente"
            $this.showAlert("Falha ao procurar Cliente", "danger");
          });
      },
      funcDesenv() {
        let $this = this;
        $this.showAlert("Função em Desenvolvimento", "info");
      },
      deleteOrcamento(tipo, id) {
        let $this = this;
        axios.get("https://localhost:44376/Orcamento/Excluir", {
          params: { "id": id }
        }).then(response => {
          if (response.data.sucess = true) {
            console.log(response.data)
            $this.showAlert(response.data.description, "success");
            this.getOrcamentos(1, 10)
          }
          else {
            $this.showAlert(response.data.description, "warning");
          }
        })
          .catch(function (error) {
            $this.showAlert("Falha ao Excluir", "danger");
          });
      },
      showModalOS(id, idcliente, tag) {
        this.buscaproduto = ''
        let $this = this;
        console.log(id, idcliente);
        this.ordemServico.idOrcamento = id;
        this.ordemServico.idCliente = idcliente;
        this.ordemServico.nome = tag;
        this.$bvModal.show("modal-2");
      },
      showModal(tipo, id) {
        let $this = this;
        axios.get("https://localhost:44376/Orcamento/GetOrcamentoById", {
          params: { "id": id }
        }).then(response => {
          console.log(response.data);
          if (response.data.sucess) {

            this.orcamento = response.data.obj.orcamento;
            this.cliente = response.data.obj.orcamento.cliente;
            this.lstprodutos = response.data.obj.lstprodutos;

            if (response.data.obj.orcamento.cliente.pessoa.tipoPessoa == 2) {
              this.selected = 'J';
            }
            else {
              this.selected = 'F';
            }
            this.$bvModal.show("modal-1");
            this.tpOperacao = tipo;
          }
          else {
            $this.showAlert("Falha ao procurar Orcamento", "danger");
          }
        })
          .catch(function (error) {
            $this.showAlert("Falha ao procurar Orcamento", "danger");
          });
      },
      addProduto(produto) {
        produto.quantidade = produto.qntd;
        produto.qntd = null;
        produto.valorItem = produto.valor;
        produto.valortotal = (produto.valor * produto.quantidade);
        this.lstprodutos.push(produto);
        //  this.produto = {};
        // this.showNovoProduto = false;
      },
      removeProduto(index) {
        this.lstprodutos.splice(index, 1);
        //  this.produto = {};
        // this.showNovoProduto = false;
      },
      showModalNovoCliente() {
        // this.showNovoCliente = !showNovoCliente;
        this.msgBuscando = '';
        this.cliente = { pessoa: {} };
        this.$bvModal.show("modal-multi-2");
      },
      sendFormOS() {
        let $this = this;
        axios.post("https://localhost:44376/Orcamento/CriaOrdemServico", this.ordemServico).then(response => {
          if (response.data.sucess == true) {
            $this.showAlert(response.data.description, "success");
            this.$bvModal.hide("modal-2")
          }
          else {
            $this.showAlert(response.data.description, "danger");
          }
        })
          .catch(function (error) {
            $this.showAlert("Falha ao Criar Ordem de Serviço", "danger");
          });
        // this will be called only after form is valid. You can do api call here to login
      },
      async sendForm() {
        let $this = this;
        const isValid = await this.$refs.formValidatorOrcamento.validate();
        console.log(isValid);
        console.log(this.cliente.id);
        if (this.cliente.id == null) {
          $this.showAlert("Informe o cliente do Orçamento", "warning");
        }
        if (isValid) {
       
          var url = "https://localhost:44376/Orcamento/Create";
          if (this.tpOperacao == "Editar") {
            url = "https://localhost:44376/Orcamento/Editar";
          }
          if (this.selected == 'J') {
            this.cliente.pessoa.tipoPessoa = 2;
          }
          else {
            this.cliente.pessoa.tipoPessoa = 1;
          }
          axios.post(url, {
            orcamento: this.orcamento, cliente: this.cliente, lstprodutos: this.lstprodutos
          }).then(response => {
            if (response.data.sucess == true) {
              //  console.log(response.data)
              $this.showAlert(response.data.description, "success");
              //window.location.href = "#/funcionarios"
              this.$bvModal.hide("modal-1")
              this.getOrcamentos(1, 10)
            }
            else {
              $this.showAlert(response.data.description, "danger");
            }
          })
            .catch(function (error) {
              $this.showAlert("Falha ao Criar/Alterar Orçamento", "danger");
            });
          // this will be called only after form is valid. You can do api call here to login
        }
      },
      async sendFormCliente() {
        const isValid = await this.$refs.formValidator.validate();
        console.log(isValid);
        if (isValid) {

          let $this = this;
          var url = "https://localhost:44376/Cliente/Create";
          if (this.selected == 'J') {
            this.cliente.pessoa.tipoPessoa = 2;
          }
          else {
            this.cliente.pessoa.tipoPessoa = 1;
          }
          axios.post(url, this.cliente).then(response => {
            if (response.data.sucess == true) {
              this.cliente = response.data.cliente;
              showBuscarCliente = false;
              $this.showAlert(response.data.description, "success");
              this.$bvModal.hide("modal-multi-2")
            }
            else {
              $this.showAlert(response.data.description, "danger");
            }
          })
            .catch(function (error) {
              $this.showAlert("Criar Cliente", "danger");
            });
        }
        // this will be called only after form is valid. You can do api call here to login
      },

      getProdutos(busca, pageN, pageS) {
        axios.get("https://localhost:44376/Produto/Index", {
          params: { "busca": busca, "pageNumber": pageN, "pageSize": pageS }
        }).then(response => {
          console.log(response.data)
          this.produtos = response.data.lst;
          // this.qntdRegistros = response.data.qntdRegistros;
        })
          .catch(function (error) {
            // alert("Falha ao Carregar Produtos");
          });
      },
      change(val) {
        this.getOrcamentos(val, 10);
      },
      cnpj(v) {
        console.log(v);
        if (v == null) {
          return '';
        }
        return v.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5")
      },
      cpf(v) {
        console.log(v);
        if (v == null) {
          return '';
        }
        v = v.replace(/\D/g, "")                    //Remove tudo o que não é dígito
        v = v.replace(/(\d{3})(\d)/, "$1.$2")       //Coloca um ponto entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d)/, "$1.$2")       //Coloca um ponto entre o terceiro e o quarto dígitos
        //de novo (para o segundo bloco de números)
        v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2") //Coloca um hífen entre o terceiro e o quarto dígitos
        return v
      }
    },
    mounted() {
      this.getOrcamentos(1, 10);
      this.getFuncionarios(1, 10);
    },
    watch: {
      currentPage: function (novo, velho) {
      },
      nomeclientebusca: function (novo, velho) {
        if (novo == '') {
          this.clientes = [];
        }
        else {
          this.buscarCliente(novo, 1, 5)
        }
      },

      buscaproduto: function (novo, velho) {
        if (novo == '') {
          this.produtos = [];
        }
        else {
          this.getProdutos(novo, 1, 5)
        }
      },
    },
    computed: {
      Sum() {
        var retorno = (parseFloat(this.orcamento.maoDeObra) + parseFloat(this.lstprodutos.reduce((Sum, product) => product.valorItem * product.quantidade + Sum, 0))).toFixed(2)
        if (isNaN(retorno)) {
          return 0;
        }
        else {
          return retorno;
        }
       
      },
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

  .tdpading05 {
    padding: 0.5rem 0.5rem !important;
    padding-top: 0.5rem !important;
    padding-right: 0.5rem !important;
    padding-bottom: 0.5rem !important;
    padding-left: 0.5rem !important;
  }
</style>
