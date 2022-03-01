<template>
  <b-card style="box-shadow: 3px 0px 5px 3px #0000007d;" no-body>
    <b-card-header class="border-0">
      <h3 class="mb-0 float-left">Orçamentos</h3>
      <base-button v-on:click="showFiltrar = !showFiltrar" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="filter-square-fill" font-scale="1"></b-icon>
        <span class="btn-inner--text">Filtrar</span>
      </base-button>
      <base-button v-b-modal.modal-1 v-on:click="tpOperacao = 'Adicionar'; cliente = { pessoa: {}}" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
        <span class="btn-inner--text">Adicionar</span>
      </base-button>
      <base-button v-on:click="funcDesenv()" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
        <b-icon icon="card-list" font-scale="1"></b-icon>
        <span class="btn-inner--text">Gerar Relatórios</span>
      </base-button>

    </b-card-header>
    <div v-if="showFiltrar" class="modal-body">
      <b-row>
        <b-col lg="3">
          <base-input type="text"
                      label="Nome"
                      placeholder="Nome"
                      v-model="filtro.nome">
          </base-input>
        </b-col>
        <b-col lg="1" v-if="selected == 'F'">
          <base-button type="success" class="float-right" style=" margin: 0; position: absolute; top: 50%; -ms-transform: translateY(-50%); transform: translateY(-50%);" v-on:click="sendForm()">
            <span class="btn-inner--text">Buscar</span>
          </base-button>
        </b-col>
      
      </b-row>
    </div>
    <el-table class="table-responsive table"
              header-row-class-name="thead-light"
              :data="orcamentos">
      <el-table-column label="Tag Identificação"
                       min-width="310px"
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
                       min-width="140px">
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

    </el-table>

    <b-card-footer class="py-4 d-flex justify-content-end">
      <base-pagination v-model="currentPage" :per-page="10" :total="qntdRegistros" @change="change"></base-pagination>
    </b-card-footer>
    <b-modal id="modal-1" title="Orçamento" size="xl">
      <b-form @submit.prevent="updateProfile">
        <h6 class="heading-small text-muted mb-4">Informações do Cliente</h6>
        <div class="">
          <b-form-group>
            <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="F">Pessoa Fisica</b-form-radio>
            <b-form-radio class="custom-control-inline" v-model="selected" name="some-radios" value="J">Pessoa Júridica</b-form-radio>
          </b-form-group>
          <b-row>
            <b-col lg="6">
              <small>
                Informe o Nome e clique em buscar para encontrar o cliente
              </small>
            </b-col>
            <b-col lg="6">
              <base-button v-on:click="buscarCliente" size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;">
                <b-icon icon="person-plus-fill" font-scale="1"></b-icon>
                <span class="btn-inner--text">Buscar</span>
              </base-button>
            </b-col>
          </b-row>
          <b-row>
            <b-col lg="6" v-if="selected == 'F'">
              <base-input type="text"
                          label="Nome"
                          placeholder="Nome"
                          v-model="cliente.pessoa.nome">
              </base-input>
            </b-col>
            <b-col lg="6" v-if="selected == 'J'">
              <base-input type="text"
                          label="Nome Fantasia"
                          placeholder="Nome Fantasia"
                          v-model="cliente.pessoa.nome">
              </base-input>
            </b-col>
            <b-col lg="6" v-if="selected == 'F'">
              <base-input type="text"
                          label="CPF"
                          placeholder="000.000.000-00"
                          v-model="cliente.pessoa.cpf">
              </base-input>
            </b-col>
            <b-col lg="6" v-if="selected == 'J'">
              <base-input type="text"
                          label="CNPJ"
                          placeholder="00.000.000/0000-00"
                          v-model="cliente.pessoa.cnpj">
              </base-input>
            </b-col>
          </b-row>

          <b-row>
            <b-col lg="6">
              <small>
                {{msgBuscando}}
              </small>
            </b-col>
          </b-row>
          <b-row v-if="!showBtBuscar">
            <b-col lg="6">
              <small>

              </small>
            </b-col>
            <b-col lg="6">
              <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px;" v-on:click="showNovoCliente = !showNovoCliente; msgBuscando = ''">
                <b-icon icon="person-plus-fill" font-scale="1"></b-icon>
                <span class="btn-inner--text">Novo Cliente</span>
              </base-button>
            </b-col>
          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="6" v-if="selected == 'F'">
              <base-input type="text"
                          label="RG"
                          placeholder="RG"
                          v-model="cliente.pessoa.rg">
              </base-input>
            </b-col>
            <b-col lg="6" v-if="selected == 'J'">
              <base-input type="text"
                          label="Razão Social"
                          placeholder="Razão Social"
                          v-model="cliente.pessoa.razaoSocial">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="E-mail"
                          placeholder="E-mail"
                          v-model="cliente.pessoa.email">
              </base-input>
            </b-col>
          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="6">
              <base-input type="text"
                          label="Telefone"
                          placeholder="Telefone"
                          v-model="cliente.pessoa.telefone">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="Telefone Celular"
                          placeholder="Telefone Celular"
                          v-model="cliente.pessoa.celular">
              </base-input>
            </b-col>
          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="12">
              <base-input type="text"
                          label="Endereço"
                          placeholder="Endereço"
                          v-model="cliente.pessoa.endereco">
              </base-input>
            </b-col>
          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="6">
              <base-input type="text"
                          label="CEP"
                          placeholder="CEP"
                          v-model="cliente.pessoa.cep">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="Número"
                          placeholder="Número"
                          v-model="cliente.pessoa.numero">
              </base-input>
            </b-col>

          </b-row>
          <b-row v-if="showNovoCliente">
            <b-col lg="6">
              <base-input type="text"
                          label="Referência"
                          placeholder="Referência"
                          v-model="cliente.pessoa.referencia">
              </base-input>
            </b-col>
            <b-col lg="6">
              <base-input type="text"
                          label="Complemento"
                          placeholder="Complemento"
                          v-model="cliente.pessoa.complemento">
              </base-input>
            </b-col>
          </b-row>
        </div>
        <hr class="my-4">
        <h6 class="heading-small text-muted mb-4">Descrição Orçamento</h6>
        <b-row>
          <b-col lg="12">
            <base-input type="text"
                        label="Tag Identificação"
                        placeholder="Tag Identificação"
                        v-model="orcamento.tagIdentificacao">
            </base-input>
          </b-col>
        </b-row>
        <b-row>
          <b-col lg="12">
            <base-input type="text"
                        label="Descrição"
                        placeholder="Descrição"
                        v-model="orcamento.descricao">
            </base-input>
          </b-col>
        </b-row>
        <hr class="my-4">

        <!-- Address -->
        <!-- Address -->
        <h6 class="heading-small text-muted mb-4">Produtos</h6>

        <div class="">
          <b-row>
            <b-col lg="12">
              <base-button size="sm" type="default" class="float-right" style="background-color: rgb(58 99 167); margin-right: 10px; margin-bottom: 10px;" v-on:click="showNovoProduto = !showNovoProduto">
                <b-icon icon="clipboard-plus" font-scale="1"></b-icon>
                <span class="btn-inner--text">Adicionar Produto</span>
              </base-button>
            </b-col>
          </b-row>
          <b-row v-if="showNovoProduto">
            <b-col lg="6">
              <base-input type="text"
                          label="Descrição"
                          placeholder="Descrição"
                          v-model="produto.descricao">
              </base-input>
            </b-col>
            <b-col lg="3">
              <base-input type="number"
                          label="Quantidade"
                          placeholder="Quantidade"
                          v-model="produto.quantidade">
              </base-input>
            </b-col>
            <b-col lg="3">
              <base-input type="number"
                          label="Valor"
                          placeholder="Valor"
                          v-model="produto.valorItem">
              </base-input>
            </b-col>
          </b-row>
          <b-row v-if="showNovoProduto">
            <b-col lg="12">
              <base-button size="sm" type="success" class="float-right" style="margin-right: 10px;" v-on:click="addProduto">
                <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
                <span class="btn-inner--text">Incluir</span>
              </base-button>
              <base-button size="sm" type="secondary" class="float-right" style="margin-right: 10px;" v-on:click="showNovoProduto = !showNovoProduto">
                <span class="btn-inner--text">Cancelar</span>
              </base-button>
            </b-col>
          </b-row>
          <b-row>
            <el-table class="table-responsive table"
                      header-row-class-name="thead-light"
                      :data="lstprodutos">
              <el-table-column label="Descrição"
                               min-width="310px"
                               prop="name">
                <template v-slot="{row}">
                  <b-media no-body class="align-items-center">
                    <b-media-body>
                      <span class="font-weight-600 name mb-0 text-sm">{{row.descricao}}</span>
                    </b-media-body>
                  </b-media>
                </template>
              </el-table-column>
              <el-table-column label="Qntd"
                               min-width="50px"
                               prop="name">
                <template v-slot="{row}">
                  <b-media no-body class="align-items-center">
                    <b-media-body>
                      <span class="font-weight-600 name mb-0 text-sm">{{row.quantidade}}</span>
                    </b-media-body>
                  </b-media>
                </template>
              </el-table-column>
              <el-table-column label="Valor Unitario"
                               min-width="310px"
                               prop="name">
                <template v-slot="{row}">
                  <b-media no-body class="align-items-center">
                    <b-media-body>
                      <span class="font-weight-600 name mb-0 text-sm">R${{row.valorItem}}</span>
                    </b-media-body>
                  </b-media>
                </template>
              </el-table-column>
              <el-table-column label="Valor Total"
                               min-width="310px"
                               prop="name">
                <template v-slot="{row}">
                  <b-media no-body class="align-items-center">
                    <b-media-body>
                      <span class="font-weight-600 name mb-0 text-sm">R${{row.valorItem * row.quantidade }}</span>
                    </b-media-body>
                  </b-media>
                </template>
              </el-table-column>
            </el-table>
          </b-row>

        </div>

        <hr class="my-4">
        <!-- Description -->
        <h6 class="heading-small text-muted mb-4">Finalização Orçamento</h6>
        <b-row>
          <b-col lg="6">
            <base-input type="text"
                        label="Valor Mão de Obra"
                        placeholder="Valor Mão de Obra"
                        v-model="orcamento.maoDeObra">
            </base-input>
          </b-col>
          <b-col lg="3">
            <label>
              Situação
            </label>
            <b-form-select v-model="orcamento.situacao" :options="options"></b-form-select>

          </b-col>
          <b-col lg="3">
            <base-input type="text"
                        label="Valor Total"
                        placeholder="Valor"
                        v-model="orcamento.valorTotal">
            </base-input>
          </b-col>
        </b-row>

      </b-form>
      <template #modal-footer="{ cancel }">
        <b-row>
          <b-col lg="12">
            <base-button type="success" class="float-right" style="margin-right: 10px;" v-on:click="sendForm()">
              <b-icon icon="plus-circle-fill" font-scale="1"></b-icon>
              <span class="btn-inner--text">{{tpOperacao}}</span>
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
        <b-row v-if="showNovoCliente">
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
        qntdRegistros: 0,
        showFiltrar: false,
        filtro: {nome: ''},
        orcamentos: [],
        options: [
          { value: 1, text: 'Aprovado' },
          { value: 2, text: 'Negado' },
          { value: 3, text: 'Aguardando' },
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
        funcionarios:[],
        lstprodutos: [],
        showBtBuscar: true,
        showNovoCliente: false,
        showNovoProduto: false,
        msgBuscando: "",
        tpOperacao: "Incluir"
      };
    },
    methods: {
      getTextoSituacao: function (situacao) {
        console.log(situacao);
        var retorno = "";
        switch (situacao) {
          case 1:
            retorno = "Aprovado";
            break;
          case 2:
            retorno ="Negado";
            break;
          case 3:
            retorno ="Aguardando";
            break;
          case 4:
            retorno ="Encerrado";
            break;
          case 5:
            retorno = "Cancelado";
            break;
        }
        return retorno;
      },
      getOrcamentos(pageN, pageS) {
        axios.get("https://localhost:44376/Orcamento/GetOrcamentos", {
          params: { "pageNumber": pageN, "pageSize": pageS }
        }).then(response => {
          console.log(response.data)
          this.orcamentos = response.data.lst;
          this.qntdRegistros = response.data.qntdRegistros;
          console.log('carrega orcamentos', this.orcamentos)
          console.log('qutnd', this.qntdRegistros)
        })
          .catch(function (error) {
            alert("Falha ao Carregar Orcamentos");
          });
      },

      getFuncionarios() {
        axios.get("https://localhost:44376/Funcionario/Grid", {
        }).then(response => {

          console.log('func', response.data);
          for (var i = 0; i < response.data.length; i++) {
            var opt = { value: response.data[i].id, text: response.data[i].pessoa.nome };
            this.funcionarios.push(opt);
          }
          console.log('lstfunc', this.funcionarios);

        })
          .catch(function (error) {
            alert("Falha ao Carregar Funcionarios");
          });
      },
      buscarCliente() {
        this.msgBuscando = "Aguarde Procurando Cliente..."
        axios.get("https://localhost:44376/Cliente/GetClienteByNome", {
          params: this.cliente.pessoa
        }).then(response => {
          if (response.data.sucess) {
            this.showBtBuscar = true;
            this.cliente = response.data.cliente;
            this.msgBuscando = response.data.description
            this.showNovoCliente = false;
          }
          else {
            this.cliente.id = 0;
            this.showBtBuscar = false;
            this.msgBuscando = response.data.description
          }
        })
          .catch(function (error) {
            this.msgBuscando = "Falha ao procurar Cliente"
            alert(error);
          });
      },
      funcDesenv() {
        alert("Função em Desenvolvimento");
      },
      deleteOrcamento(tipo, id) {
        axios.get("https://localhost:44376/Orcamento/Excluir", {
          params: { "id": id }
        }).then(response => {
          if (response.data.sucess = true) {
            console.log(response.data)
            alert(response.data.description)
            this.getOrcamentos(1,10)
          }
          else {
            alert(response.data.description)
          }
        })
          .catch(function (error) {
            alert(error);
          });
      },
      showModalOS(id, idcliente, tag) {
        console.log(id, idcliente);
        this.ordemServico.idOrcamento = id;
        this.ordemServico.idCliente = idcliente;
        this.ordemServico.nome = tag;
        this.$bvModal.show("modal-2");
      },
      showModal(tipo, id) {
        axios.get("https://localhost:44376/Orcamento/GetOrcamentoById", {
          params: { "id": id}
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
              alert("Falha ao procurar Orcamento");
            }
          })
            .catch(function (error) {
              alert("Falha ao procurar Orcamento");
            });
      },
      addProduto() {
        this.produto.valortotal = (this.produto.valoritem * this.produto.quantidade);
        this.lstprodutos.push(this.produto);
        this.produto = {};
        this.showNovoProduto = false;
      },
      sendFormOS() {

        axios.post("https://localhost:44376/Orcamento/CriaOrdemServico", this.ordemServico).then(response => {
          if (response.data.sucess = true) {
            alert(response.data.description)
            this.$bvModal.hide("modal-2")
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
      sendForm() {
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
          if (response.data.sucess = true) {
            console.log(response.data)
            alert(response.data.description)
            //window.location.href = "#/funcionarios"
            this.$bvModal.hide("modal-1")
            this.getOrcamentos(1,10)
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
      change(val) {
        this.getOrcamentos(val, 10);
      }
    },
    mounted() {
      this.getOrcamentos(1,10);
     // this.getFuncionarios();
    },
    watch: {
      currentPage: function (novo, velho) {
        console.log('teste');
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
</style>
