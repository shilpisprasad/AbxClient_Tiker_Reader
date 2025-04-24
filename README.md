# ABX Client Ticker Reader

This repository contains a C# client application that connects to a stock exchange server (ABX Exchange Server) running on Node.js. The client requests ticker data, handles missing packets, and outputs a complete JSON file with all received ticker data in sequence.

## Project Structure

```
AbxClient_Tiker_Reader/
├── ABXClient/              # C# client app (.NET Core)
├── abx_exchange_server/    # Node.js ABX Exchange Server
│   └── main.js             # Server entry point
└── README.md
```

## How to Run

### Start the ABX Exchange Server

1. Clone the repository:
```bash
git clone https://github.com/shilpisprasad/AbxClient_Tiker_Reader.git
cd AbxClient_Tiker_Reader
```

2. You should have Node.js (>=16.17.0) installed.

3. Run the server:
```bash
cd abx_exchange_server
node main.js
```

### Run the C# ABX Client

1. In a new terminal:
```bash
cd ABXClient
```

2. You should have .NET SDK installed.

3. Run the client:
```bash
dotnet run
```

## Output

The client generates a file `output.json` inside the `ABXClient\bin\Debug\net8.0` folder containing all ticker data from the server.
