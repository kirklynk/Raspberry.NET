## Welcome to Relay PI App

This application is designed to interface with the 4 port relay module hardware, providing users with a seamless experience to control and monitor their relay modules.

### Features

This is a simple application that allows you to:

- Control up to 4 relay ports
- Monitor the status of each relay port
- User-friendly interface for easy operation
- Lightweight and efficient performance
- Web-based control panel


### Requirements
- .NET 10.0 SDK installed on your development machine
- Raspberry Pi with .NET 10.0 support, ie Pi Zero 2W, Pi 2, Pi 3, Pi 4, Pi 400
- Latest Raspbian OS installed
- 4 Port Relay Module compatible with Raspberry Pi (eg. Inland 4-Channel 5V Relay Module Hat from Microcenter)

### Installation
1. Clone the repository using Visual Studio 2026 or VS Code.
2. Open the solution file `RelayPIApp.slnx`.
3. Build the solution to restore dependencies and compile the application.
4. Deploy the application to a folder on your development machine,ensuring that target framework is linux-arm64 for 64bit Raspbian. The deployment mode needs to be set to `Self Contained`, this ensure that you can run the application on your Raspberry Pi without any additional installation.
5. Log in to your Raspberry Pi via SSH or directly using a monitor and keyboard.
6. Create a directory for the application, e.g., `mkdir ~/home/pi/app`. Do not use Sudo for this step.
7. Copy the contents of deployed folder to your Raspberry Pi using SCP, SFTP, or a USB drive to this directory. I used this powershell scp command, `scp -r c:\temp pi@relaypi.local:/home/pi/app`. This will be different for you as my application is deployed to `c:\temp` on my development machine and my Pi is named relaypi.local.
8. Once the contents are copied to the Pi directory. SSH into the PI and change into the directory we copied the contents to on the Pi. This is because we need to make the application executable. Run the command `sudo chmod +x RelayPIApp`.
9. Now you can test run the application using the command `./RelayPIApp`. You should see output indicating that the web server is running and listening on a specific port (default is 5000). Optionally, you can specify a different `sudo ./RelayPIApp --urls "http://+:80` to run the application on port 80.
10. Open a web browser on your development machine and navigate to `http://<Raspberry_Pi_IP_Address>:5000` (or the port you specified) to access the Relay PI App web interface. You should see the Relay PI App interface where you can control and monitor the relay ports.
11. To stop the application, you can simply press `Ctrl + C` in the terminal where the application is running.
12. (Optional) To run the application as a background service, you can create a systemd service file. This step is optional but recommended for ease of use. You can copy the `RelayPIApp.service` file from the `docs` folder in the repository to the Pi's `/etc/systemd/system/` and modify it as needed.
	- Go to the `/etc/systemd/system/` directory on the Pi using `cd /etc/systemd/system/`
	- Run `sudo nano RelayPIApp.service` to edit the service file if needed.
	- Modify the `ExecStart` line to point to the correct location of the `RelayPIApp` executable on your Pi.
	- Modify the `WorkingDirectory` line to point to the directory where the application is located.
	- Save the changes and exit the editor.
	- Enable the service to start on boot with the command `sudo systemctl enable RelayPIApp.service`.
	- Start the service with the command `sudo systemctl start RelayPIApp.service`.
	- Check the status of the service with the command `sudo systemctl status RelayPIApp.service`.

### Usage
- Hook up the 4 port relay module to the Raspberry Pi according to the relay module's wiring diagram.
- Access the web interface using a web browser by navigating to `http://<Raspberry_Pi_IP_Address>:5000` (or the port you specified). You should be able to control and monitor the relay ports from this interface.