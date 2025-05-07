# 🎵 Rhythm Sphere - Jogo Musical em Unity 🎶

![Capa do Jogo](https://via.placeholder.com/800x400?text=Rhythm+Sphere+Capa) *(imagem ilustrativa)*

## 📌 Visão Geral
Jogo rítmico desenvolvido em Unity onde os jogadores devem clicar em esferas coloridas que caem sincronizadas com a música. O jogo analisa o espectro de áudio em tempo real para gerar padrões de jogo dinâmicos.

## 🕹️ Mecânicas Principais

### 🎯 Sistema de Esferas
- 4 cores de esferas correspondentes a diferentes canais de áudio
- Padrões de spawn baseados na análise espectral:
  ```csharp
  // Exemplo de thresholds para spawn
  private float[] valueMin_Normal = {0.06f, 0.16f};
  private float[] valueMax_Normal = {0.1f, 0.3f};
