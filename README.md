# ABX Client Ticker Reader

This repository contains a C# client application that connects to a mock stock exchange server (ABX Exchange Server) running on Node.js. The client requests ticker data, handles missing packets, and outputs a complete JSON file with all received ticker data in sequence.

---

## 📁 Project Structure

```
AbxClient_Tiker_Reader/
├── ABXClient/              # C# client app (.NET Core)
├── exchange_server/        # Node.js mock ABX Exchange Server
│   └── main.js             # Server entry point
└── README.md
```

---

## 🛠 How to Run Locally

### 1️⃣ Start the ABX Exchange Server

1. Clone the repository:
```bash
git clone https://github.com/shilpisprasad/AbxClient_Tiker_Reader.git
cd AbxClient_Tiker_Reader
```

2. Install Node.js (version 16.17.0 or later):  
➡️ https://nodejs.org/en/download

3. Navigate to the exchange server folder and run the server:
```bash
cd exchange_server
node main.js
```
> 📌 This will start the ABX Exchange Server on port 3000

🔁 Leave this terminal running while you run the C# client.

---

### 2️⃣ Run the C# ABX Client

1. Open a new terminal window or tab.

2. Navigate to the C# client folder:
```bash
cd AbxClient_Tiker_Reader/ABXClient
```

3. Run the client:
```bash
dotnet run
```
> 📝 Make sure you have the .NET SDK installed:  
➡️ https://dotnet.microsoft.com/download

---

## 📤 Output

The client generates a file called `output.json` inside the `ABXClient` folder.  
This JSON file contains all packets from the server with no missing sequences.

---

## 🧪 Notes

- The client uses TCP protocol to communicate with the server on `localhost:3000`
- It handles "Stream All Packets" and detects/resends any missing packets using the "Resend Packet" call
- All responses are parsed and validated using Big Endian format as per server spec

---

## 📮 Submission

Once completed, fill and submit the Google Form:  
👉 https://forms.gle/V8F1hf68cJEN8kAM9

Please keep your repository **public** and make sure this README is up to date.

---

## 🙋‍♀️ Author

🔗 GitHub: [shilpisprasad](https://github.com/shilpisprasad)
