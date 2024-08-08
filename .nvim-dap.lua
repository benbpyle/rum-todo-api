local dap = require("dap")

dap.adapters.lldb = {
	type = "executable",
	command = "/usr/bin/lldb", -- adjust as needed
	name = "lldb",
	options = {
		env = {
			"BIND_ADDRESS=0.0.0.0:3000",
			"RUST_LOG=info",
			"DATABASE_URL=postgresql://postgres:mysecretpassword@localhost:5432/postgres",
			"AGENT_ADDRESS=127.0.0.1",
		},
	},
}

dap.configurations.rust = {
	{
		name = "todos-api",
		type = "lldb",
		request = "launch",
		program = function()
			return vim.fn.getcwd() .. "/target/debug/todos"
		end,
		cwd = "${workspaceFolder}",
		stopOnEntry = false,
	},
}
