<template>
  <div>

    <base-header class="pb-6 pb-8 pt-5 pt-md-8 bg-gradient-success">
      <!-- Card stats -->
      <b-row>
        <b-row>
          <b-col xl="5" md="6">
            <stats-card title="Orçamentos"
                        type="gradient-red"
                        :sub-title="orcamentosAprovados + ' Aprovado(s)'"
                        icon="ni ni-chart-bar-32"
                        class="mb-4">

              <template slot="footer">
                <a href="#/orcamentos" class="card-link">Todos os orçamentos</a>
              </template>
            </stats-card>
          </b-col>
          <b-col xl="5" md="6">
            <stats-card title="Orçamentos"
                        type="gradient-red"
                        :sub-title="orcamentosAguardando +  ' Aguardando aprovação'"
                        icon="ni ni-chart-bar-32"
                        class="mb-4">

              <template slot="footer">
                <a href="#/orcamentos" class="card-link">Todos os orçamentos</a>
              </template>
            </stats-card>
          </b-col>
        </b-row>
        <b-row>
          <b-col xl="5" md="6">
            <stats-card title="Ordens de Serviço"
                        type="gradient-orange"
                        :sub-title="ordemServicoAberto + ' Abertas'"
                        icon="ni ni-chart-pie-35"
                        class="mb-4">

              <template slot="footer">
                <a href="#/ordemServico" class="card-link">Todos as ordens de serviço</a>
              </template>
            </stats-card>
          </b-col>
          <b-col xl="5" md="6">
            <stats-card title="Ordens de Serviço"
                        type="gradient-orange"
                        :sub-title="ordemServicoFechado + ' Fechadas'"
                        icon="ni ni-chart-pie-35"
                        class="mb-4">

              <template slot="footer">
                <a href="#/ordemServico" class="card-link">Todos as ordens de serviço</a>
              </template>
            </stats-card>
          </b-col>
        </b-row>
        <b-row>
          <b-col xl="5" md="6">
            <stats-card title="Clientes"
                        type="gradient-info"
                        :sub-title="clientes + ' Cadastrados'"
                        icon="ni ni-single-02"
                        class="mb-4">

              <template slot="footer">
                <a href="#/clientes" class="card-link">Todos os clientes</a>
              </template>
            </stats-card>
          </b-col>
          <b-col xl="5" md="6">
            <stats-card title="Funcionários"
                        type="gradient-info"
                        :sub-title="funcionarios + ' Cadastrados'"
                        icon="ni ni-circle-08"
                        class="mb-4">

              <template slot="footer">
                <a href="#/funcionarios" class="card-link">Todos os funcionários</a>
              </template>
            </stats-card>
          </b-col>
        </b-row>
      </b-row>
    </base-header>

  </div>
</template>
<script>
  // Charts
  import * as chartConfigs from '@/components/Charts/config';
  import LineChart from '@/components/Charts/LineChart';
  import BarChart from '@/components/Charts/BarChart';

  // Components
  import BaseProgress from '@/components/BaseProgress';
  import StatsCard from '@/components/Cards/StatsCard';

  // Tables
  import SocialTrafficTable from './Dashboard/SocialTrafficTable';
  import PageVisitsTable from './Dashboard/PageVisitsTable';

  import axios from 'axios'

  export default {
    components: {
      LineChart,
      BarChart,
      BaseProgress,
      StatsCard,
      PageVisitsTable,
      SocialTrafficTable
    },
    data() {
      return {
        orcamentosAprovados: [],
        orcamentosAguardando: [],
        ordemServicoAberto: [],
        ordemServicoFechado: [],
        funcionarios: [],
        clientes: [],
        forecasts: [],
        bigLineChart: {
          allData: [
            [0, 20, 10, 30, 15, 40, 20, 60, 60],
            [0, 20, 5, 25, 10, 30, 15, 40, 40]
          ],
          activeIndex: 0,
          chartData: {
            datasets: [
              {
                label: 'Performance',
                data: [0, 20, 10, 30, 15, 40, 20, 60, 60],
              }
            ],
            labels: ['May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
          },
          extraOptions: chartConfigs.blueChartOptions,
        },
        redBarChart: {
          chartData: {
            labels: ['Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            datasets: [{
              label: 'Sales',
              data: [25, 20, 30, 22, 17, 29]
            }]
          },
          extraOptions: chartConfigs.blueChartOptions
        }
      };
    },
    methods: {
      
      GetOrcamento() {
        axios.get("https://localhost:44376/Orcamento/GetOrcamentoSituacao").then(response => {
          
          this.orcamentosAprovados = response.data.orcamentos;
          this.orcamentosAguardando = response.data.orcamentosAguardando
            console.log(this.orcamentos)
          
        })
          .catch(function (error) {
            //this.showAlert("Falha ao carregar orçamentos", "danger");
          });
      },

      GetOrdemServico() {
        axios.get("https://localhost:44376/OrdemServico/GetOrdemServicoSituacao").then(response => {

          this.ordemServicoAberto = response.data.ordemAberta
          this.ordemServicoFechado = response.data.ordemFechada

        })
          .catch(function (error) {
            //this.showAlert("Falha ao carregar orçamentos", "danger");
          });
      },

      GetClientes() {
        axios.get("https://localhost:44376/Cliente/GetTotalCliente").then(response => {

          this.clientes = response.data

        })
          .catch(function (error) {
          });
      },

      GetFuncionarios() {
        axios.get("https://localhost:44376/Funcionario/GetTotalFuncionario").then(response => {

          this.funcionarios = response.data

        })
          .catch(function (error) {
          });
      },

      initBigChart(index) {
        let chartData = {
          datasets: [
            {
              label: 'Performance',
              data: this.bigLineChart.allData[index]
            }
          ],
          labels: ['May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        };
        this.bigLineChart.chartData = chartData;
        this.bigLineChart.activeIndex = index;
      },
    
    },
    mounted() {
      this.GetOrcamento();
      this.GetOrdemServico();
      this.GetClientes();
      this.GetFuncionarios();
    }
  };
</script>
<style>
.el-table .cell{
  padding-left: 0px;
  padding-right: 0px;
}
</style>
