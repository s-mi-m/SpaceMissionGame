using System;

class AIresponse
{
    public static string[] GetMissionName()
    {
        return new string[]
        {
            "Europa Clipper",
            "Psyche",
            "Dragonfly",
            "NISAR",
            "SPHEREx"
        };
    }

    public static string[] GetMissionObjective()
    {
        return new string[]
        {
            "Study Europa and investigate whether conditions suitable for life exist.",
            "Study the composition and properties of the metallic asteroid Psyche.",
            "Explore Titan and investigate its surface and atmosphere.",
            "Measure changes on Earth's land and ice surfaces using radar.",
            "Survey the sky in optical and near-infrared wavelengths."
        };
    }

    public static string[] GetMissionObstacle()
    {
        return new string[]
        {
            "Long-distance communication and limited spacecraft resources.",
            "Deep-space travel and limited power and communication resources.",
            "Titan's atmosphere and the need for descent and landing.",
            "Large amounts of Earth observation data and precise orbit requirements.",
            "Wide-area sky survey while managing power, mass and data."
        };
    }

    public static int GetBudgetFromTheOrganizer(int money)
    {
        Userprofile.budget = money;
        return money;
    }
}


class Userprofile
{
    public static string name;
    public static int budget;
}


class LaunchVehicle
{
    public string[] name =
    {
        "Pegasus",
        "Taurus",
        "Delta II",
        "SLS Block 1"
    };

    public double[] payloadCapacity =
    {
        400,
        1350,
        6000,
        27000
    };

    public int[] price =
    {
        100,
        150,
        250,
        500
    };

    public int selectedVehicle = -1;

    public bool ChooseLaunchVehicle()
    {
        Console.WriteLine();
        Console.WriteLine("===== LAUNCH VEHICLE =====");

        for (int i = 0; i < name.Length; i++)
        {
            Console.WriteLine();
            Console.WriteLine(i + ". " + name[i]);
            Console.WriteLine("Payload Capacity: " +
                              payloadCapacity[i] + " kg");
            Console.WriteLine("Cost: $" +
                              price[i] + "M");
        }

        Console.WriteLine();
        Console.Write("Choose launch vehicle: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice < 0 || choice >= name.Length)
        {
            Console.WriteLine("Invalid launch vehicle.");
            return false;
        }

        if (price[choice] > Userprofile.budget)
        {
            Console.WriteLine("OOPS! Not enough budget.");
            return false;
        }

        selectedVehicle = choice;

        Userprofile.budget -= price[choice];

        Console.WriteLine();
        Console.WriteLine("Launch Vehicle Selected: " +
                          name[choice]);

        Console.WriteLine("Launch Cost: $" +
                          price[choice] + "M");

        Console.WriteLine("Remaining Budget: $" +
                          Userprofile.budget + "M");

        return true;
    }

    public bool CheckPayload(double spacecraftMass)
    {
        if (selectedVehicle == -1)
        {
            return false;
        }

        if (spacecraftMass > payloadCapacity[selectedVehicle])
        {
            Console.WriteLine();
            Console.WriteLine(
                "This launch vehicle cannot carry the spacecraft."
            );

            Console.WriteLine(
                "Required: " + spacecraftMass + " kg"
            );

            Console.WriteLine(
                "Vehicle capacity: " +
                payloadCapacity[selectedVehicle] + " kg"
            );

            return false;
        }

        return true;
    }
}


class SpaceCraft
{
    public string[] name =
    {
        "Structure",
        "Power",
        "Thermal Control",
        "Propulsion",
        "Attitude Control",
        "Guidance & Navigation",
        "Communication",
        "Command & Data Handling",
        "Flight Computer / Avionics",
        "Data Storage",
        "Scientific Instruments / Payload",
        "Mechanical / Deployment Systems",
        "Wiring / Electrical Distribution",
        "Radiation Protection",
        "Entry, Descent & Landing (EDL)",
        "Docking / Crew Systems"
    };

    public int[] price =
    {
        100, 60, 50, 100,
        40, 40, 70, 40,
        50, 15, 200, 40,
        20, 30, 150, 200
    };

    public double[] componentMass =
    {
        40, 35, 25, 45,
        20, 15, 20, 18,
        15, 8, 10, 12,
        8, 15, 30, 35
    };

    public double massCapacity = 373.0;
    public double powerCapacity = 129.8;

    public bool[] selected = new bool[16];

    public bool PurchaseComponent(int index)
    {
        if (index < 0 || index >= name.Length)
        {
            Console.WriteLine("Invalid component.");
            return false;
        }

        if (selected[index])
        {
            Console.WriteLine("This component is already selected.");
            return false;
        }

        if (price[index] > Userprofile.budget)
        {
            Console.WriteLine("OOPS! Not enough budget.");
            return false;
        }

        if (componentMass[index] > massCapacity)
        {
            Console.WriteLine("OOPS! Not enough mass capacity.");
            return false;
        }

        Userprofile.budget -= price[index];
        massCapacity -= componentMass[index];

        selected[index] = true;

        Console.WriteLine();
        Console.WriteLine("Selected: " + name[index]);
        Console.WriteLine("Cost: $" + price[index] + "M");
        Console.WriteLine("Mass Used: " +
                          componentMass[index] + " kg");
        Console.WriteLine("Remaining Budget: $" +
                          Userprofile.budget + "M");
        Console.WriteLine("Remaining Mass Capacity: " +
                          massCapacity + " kg");

        return true;
    }

    public bool DesignMandatorySystems(string objective)
    {
        Console.WriteLine();
        Console.WriteLine("===== MANDATORY SPACECRAFT SYSTEMS =====");

        int[] core =
        {
            0, 1, 2, 4, 5, 6, 7, 8, 12
        };

        for (int i = 0; i < core.Length; i++)
        {
            Console.WriteLine();
            Console.WriteLine("Required: " + name[core[i]]);

            if (!PurchaseComponent(core[i]))
            {
                Console.WriteLine(
                    "Mission cannot continue without this system."
                );

                return false;
            }
        }

        if (objective.Contains("Europa") ||
            objective.Contains("Psyche") ||
            objective.Contains("NISAR") ||
            objective.Contains("SPHEREx"))
        {
            Console.WriteLine();
            Console.WriteLine("Propulsion is required.");

            if (!PurchaseComponent(3))
            {
                return false;
            }
        }

        if (objective.Contains("Titan"))
        {
            Console.WriteLine();
            Console.WriteLine(
                "Entry, Descent & Landing is required."
            );

            if (!PurchaseComponent(14))
            {
                return false;
            }

            Console.WriteLine();
            Console.WriteLine("Propulsion is required.");

            if (!PurchaseComponent(3))
            {
                return false;
            }
        }

        return true;
    }

    public void ChooseOptionalSystems()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== OPTIONAL SPACECRAFT SYSTEMS =====");

            Console.WriteLine(
                "9. Data Storage - $" + price[9] + "M"
            );

            Console.WriteLine(
                "11. Mechanical / Deployment Systems - $" +
                price[11] + "M"
            );

            Console.WriteLine(
                "13. Radiation Protection - $" +
                price[13] + "M"
            );

            Console.WriteLine(
                "15. Docking / Crew Systems - $" +
                price[15] + "M"
            );

            Console.WriteLine("-1. Continue");

            Console.Write("Choose: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == -1)
            {
                break;
            }

            if (choice == 9 ||
                choice == 11 ||
                choice == 13 ||
                choice == 15)
            {
                PurchaseComponent(choice);
            }
            else
            {
                Console.WriteLine("Invalid optional component.");
            }
        }
    }
}


class Communication
{
    public string[] name =
    {
        "Low Gain Antenna",
        "Medium Gain Antenna",
        "High Gain Antenna",
        "Deep Space Communication System"
    };

    public int[] price =
    {
        20,
        40,
        70,
        100
    };

    public double[] powerRequired =
    {
        5,
        10,
        20,
        30
    };

    public bool ChooseCommunication(
        SpaceCraft spacecraft,
        string mission)
    {
        Console.WriteLine();
        Console.WriteLine("===== COMMUNICATION SYSTEM =====");

        bool deepSpace =
            mission == "Europa Clipper" ||
            mission == "Psyche" ||
            mission == "Dragonfly";

        if (deepSpace)
        {
            Console.WriteLine(
                "This mission requires the Deep Space Communication System."
            );

            int choice = 3;

            if (price[choice] > Userprofile.budget)
            {
                Console.WriteLine("OOPS! Not enough budget.");
                return false;
            }

            if (powerRequired[choice] >
                spacecraft.powerCapacity)
            {
                Console.WriteLine(
                    "OOPS! Not enough power capacity."
                );

                return false;
            }

            Userprofile.budget -= price[choice];

            spacecraft.powerCapacity -=
                powerRequired[choice];

            Console.WriteLine();
            Console.WriteLine(
                "Communication Selected: " +
                name[choice]
            );

            Console.WriteLine(
                "Cost: $" + price[choice] + "M"
            );

            Console.WriteLine(
                "Remaining Budget: $" +
                Userprofile.budget + "M"
            );

            Console.WriteLine(
                "Remaining Power Capacity: " +
                spacecraft.powerCapacity + " W"
            );

            return true;
        }

        for (int i = 0; i < name.Length - 1; i++)
        {
            Console.WriteLine();
            Console.WriteLine(i + ". " + name[i]);

            Console.WriteLine(
                "Cost: $" + price[i] + "M"
            );

            Console.WriteLine(
                "Power Required: " +
                powerRequired[i] + " W"
            );
        }

        Console.WriteLine();
        Console.Write("Choose communication system: ");

        int selected = Convert.ToInt32(Console.ReadLine());

        if (selected < 0 || selected > 2)
        {
            Console.WriteLine("Invalid communication system.");
            return false;
        }

        if (price[selected] > Userprofile.budget)
        {
            Console.WriteLine("OOPS! Not enough budget.");
            return false;
        }

        if (powerRequired[selected] >
            spacecraft.powerCapacity)
        {
            Console.WriteLine(
                "OOPS! Not enough power capacity."
            );

            return false;
        }

        Userprofile.budget -= price[selected];

        spacecraft.powerCapacity -=
            powerRequired[selected];

        Console.WriteLine();
        Console.WriteLine(
            "Communication Selected: " +
            name[selected]
        );

        Console.WriteLine(
            "Remaining Budget: $" +
            Userprofile.budget + "M"
        );

        return true;
    }
}


class Instruments
{
    public static void GetTheInstrumentTools(
        string[] instrumentLibrary,
        string[] purpose,
        int[] costs,
        double[] mass,
        double[] power,
        string[] powerDisplay,
        string[] data,
        string[] suitableFor,
        SpaceCraft spacecraft)
    {
        int selectedCount = 0;

        while (selectedCount < 2)
        {
            Console.WriteLine();
            Console.WriteLine(
                "===== CHOOSE INSTRUMENT " +
                (selectedCount + 1) +
                " ====="
            );

            DisplayInstruments(
                instrumentLibrary,
                purpose,
                costs,
                mass,
                power,
                powerDisplay,
                data,
                suitableFor
            );

            Console.Write("Choose instrument number: ");

            int choice =
                Convert.ToInt32(Console.ReadLine());

            if (SelectInstrument(
                choice,
                instrumentLibrary,
                costs,
                mass,
                power,
                spacecraft))
            {
                selectedCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine(
            "Minimum 2 instruments selected."
        );

        while (true)
        {
            Console.WriteLine();
            Console.Write(
                "Do you want to add another instrument? (yes/no): "
            );

            string answer =
                Console.ReadLine().ToLower();

            if (answer == "no")
            {
                break;
            }

            if (answer != "yes")
            {
                Console.WriteLine(
                    "Please enter yes or no."
                );

                continue;
            }

            DisplayInstruments(
                instrumentLibrary,
                purpose,
                costs,
                mass,
                power,
                powerDisplay,
                data,
                suitableFor
            );

            Console.Write(
                "Choose instrument number: "
            );

            int choice =
                Convert.ToInt32(Console.ReadLine());

            SelectInstrument(
                choice,
                instrumentLibrary,
                costs,
                mass,
                power,
                spacecraft
            );
        }

        Console.WriteLine();
        Console.WriteLine(
            "===== INSTRUMENT SELECTION COMPLETED ====="
        );

        Console.WriteLine(
            "Total instruments selected: " +
            selectedCount
        );

        Console.WriteLine(
            "Remaining Budget: $" +
            Userprofile.budget + "M"
        );

        Console.WriteLine(
            "Remaining Mass Capacity: " +
            spacecraft.massCapacity + " kg"
        );

        Console.WriteLine(
            "Remaining Power Capacity: " +
            spacecraft.powerCapacity + " W"
        );
    }

    public static void DisplayInstruments(
        string[] instrumentLibrary,
        string[] purpose,
        int[] costs,
        double[] mass,
        double[] power,
        string[] powerDisplay,
        string[] data,
        string[] suitableFor)
    {
        for (int i = 0;
             i < instrumentLibrary.Length;
             i++)
        {
            Console.WriteLine();
            Console.WriteLine(
                i + ". " + instrumentLibrary[i]
            );

            Console.WriteLine(
                "Purpose: " + purpose[i]
            );

            Console.WriteLine(
                "Cost: $" + costs[i] + "M"
            );

            Console.WriteLine(
                "Mass: " + mass[i] + " kg"
            );

            Console.WriteLine(
                "Power: " + powerDisplay[i]
            );

            Console.WriteLine(
                "Data: " + data[i]
            );

            Console.WriteLine(
                "Suitable for: " + suitableFor[i]
            );
        }
    }

    public static bool SelectInstrument(
        int choice,
        string[] instrumentLibrary,
        int[] costs,
        double[] mass,
        double[] power,
        SpaceCraft spacecraft)
    {
        if (choice < 0 ||
            choice >= instrumentLibrary.Length)
        {
            Console.WriteLine(
                "Invalid instrument selection."
            );

            return false;
        }

        if (costs[choice] > Userprofile.budget)
        {
            Console.WriteLine(
                "OOPS! Not enough budget."
            );

            return false;
        }

        if (mass[choice] >
            spacecraft.massCapacity)
        {
            Console.WriteLine(
                "OOPS! Not enough mass capacity."
            );

            return false;
        }

        if (power[choice] >
            spacecraft.powerCapacity)
        {
            Console.WriteLine(
                "OOPS! Not enough power capacity."
            );

            return false;
        }

        Userprofile.budget -= costs[choice];

        spacecraft.massCapacity -=
            mass[choice];

        spacecraft.powerCapacity -=
            power[choice];

        Console.WriteLine();
        Console.WriteLine(
            "Instrument added successfully."
        );

        Console.WriteLine(
            "Selected: " +
            instrumentLibrary[choice]
        );

        Console.WriteLine(
            "Cost: $" +
            costs[choice] + "M"
        );

        Console.WriteLine(
            "Remaining Budget: $" +
            Userprofile.budget + "M"
        );

        Console.WriteLine(
            "Remaining Mass Capacity: " +
            spacecraft.massCapacity + " kg"
        );

        Console.WriteLine(
            "Remaining Power Capacity: " +
            spacecraft.powerCapacity + " W"
        );

        return true;
    }
}


class OrbitalConstraints
{
    public void ChooseOrbit(string mission)
    {
        Console.WriteLine();
        Console.WriteLine("===== ORBIT / TRAJECTORY =====");

        if (mission == "NISAR")
        {
            Console.WriteLine(
                "Mission requires an Earth observation orbit."
            );
        }
        else if (mission == "SPHEREx")
        {
            Console.WriteLine(
                "Mission requires an Earth orbit for its sky survey."
            );
        }
        else if (mission == "Europa Clipper")
        {
            Console.WriteLine(
                "Mission requires a deep-space trajectory toward Jupiter."
            );
        }
        else if (mission == "Psyche")
        {
            Console.WriteLine(
                "Mission requires an interplanetary trajectory."
            );
        }
        else if (mission == "Dragonfly")
        {
            Console.WriteLine(
                "Mission requires an interplanetary trajectory to Titan."
            );
        }
    }
}

class MissionTrajectory
{
    public string missionName;
    public string objective;
    public string destination;
    public string trajectory1;
    public string trajectory2;

    public void ShowTrajectory()
    {
        Console.WriteLine();
        Console.WriteLine("===== MISSION TRAJECTORY =====");

        Console.WriteLine("Mission: " + missionName);
        Console.WriteLine("Objective: " + objective);
        Console.WriteLine("Destination: " + destination);

        Console.WriteLine();
        Console.WriteLine("Two closest trajectory options found:");

        Console.WriteLine("1. " + trajectory1);
        Console.WriteLine("2. " + trajectory2);
    }
}


class MissionStatus
{
    public double distance;
    public double fuel;
    public double battery;
    public double trajectoryDeviation;
    public double dataStorage;
    public double scienceData;

    public bool propulsionDamaged;
    public bool communicationDamaged;
    public bool instrumentDamaged;
    public bool missionAlive;
    public bool researchCompleted;
    public bool sampleCollected;

    public MissionStatus()
    {
        distance = 100;
        fuel = 100;
        battery = 100;
        trajectoryDeviation = 0;
        dataStorage = 0;
        scienceData = 0;

        propulsionDamaged = false;
        communicationDamaged = false;
        instrumentDamaged = false;

        missionAlive = true;
        researchCompleted = false;
        sampleCollected = false;
    }

    public void ShowStatus()
    {
        Console.WriteLine();
        Console.WriteLine("----- CURRENT MISSION STATUS -----");

        Console.WriteLine(
            "Distance to destination: " +
            distance.ToString("0.0") + "%"
        );

        Console.WriteLine(
            "Fuel: " +
            fuel.ToString("0.0") + "%"
        );

        Console.WriteLine(
            "Battery: " +
            battery.ToString("0.0") + "%"
        );

        Console.WriteLine(
            "Trajectory deviation: " +
            trajectoryDeviation.ToString("0.0") + "%"
        );

        Console.WriteLine(
            "Data storage: " +
            dataStorage.ToString("0.0") + "%"
        );

        Console.WriteLine(
            "Science data: " +
            scienceData.ToString("0.0") + "%"
        );
    }
}


class MissionSimulation
{
    private string missionName;
    private string objective;

    private SpaceCraft spacecraft;
    private MissionTrajectory trajectory;
    private MissionStatus status;

    private Random random;


    public MissionSimulation(
        string missionName,
        string objective,
        SpaceCraft spacecraft,
        MissionTrajectory trajectory)
    {
        this.missionName = missionName;
        this.objective = objective;
        this.spacecraft = spacecraft;
        this.trajectory = trajectory;

        status = new MissionStatus();
        random = new Random();
    }


    
    public void RunSimulation()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "           POST-LAUNCH MISSION"
        );

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine();
        Console.WriteLine(
            "Mission has successfully left Earth."
        );

        InitialSystemCheck();

        if (!status.missionAlive)
        {
            FinalReport();
            return;
        }

        EarthDeparture();

        if (!status.missionAlive)
        {
            FinalReport();
            return;
        }

        CruiseToDestination();

        if (!status.missionAlive)
        {
            FinalReport();
            return;
        }

        DestinationOperations();

        if (!status.missionAlive)
        {
            FinalReport();
            return;
        }

        ReturnJourney();

        if (!status.missionAlive)
        {
            FinalReport();
            return;
        }

        EarthArrival();

        FinalReport();
    }


    private void InitialSystemCheck()
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== INITIAL SYSTEM CHECK ====="
        );

        Console.WriteLine(
            "Checking propulsion..."
        );

        Console.WriteLine(
            "Checking power..."
        );

        Console.WriteLine(
            "Checking communication..."
        );

        Console.WriteLine(
            "Checking navigation..."
        );

        Console.WriteLine(
            "Checking scientific instruments..."
        );

        Console.WriteLine();

        Console.WriteLine(
            "All primary systems are operational."
        );

        Console.WriteLine(
            "Mission control gives permission to depart."
        );

        status.ShowStatus();
    }


   
    private void EarthDeparture()
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== EARTH DEPARTURE ====="
        );

        Console.WriteLine(
            "Spacecraft is beginning its departure maneuver."
        );

        Console.WriteLine(
            "Main propulsion system activated."
        );

        status.fuel -= 10;
        status.battery -= 5;

        Console.WriteLine(
            "Departure maneuver completed."
        );

        Console.WriteLine(
            "Fuel used: 10%"
        );

        Console.WriteLine(
            "Battery used: 5%"
        );

        status.distance = 90;

        status.ShowStatus();
    }


   
    private void CruiseToDestination()
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== DEEP SPACE CRUISE ====="
        );

        Console.WriteLine(
            "Spacecraft is travelling toward:"
        );

        Console.WriteLine(
            trajectory.destination
        );

        Console.WriteLine();

       
        for (int stage = 1; stage <= 3; stage++)
        {
            if (!status.missionAlive)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine(
                "--- Cruise Stage " + stage + " ---"
            );

            status.distance -= 15;
            status.fuel -= 3;
            status.battery -= 4;

            Console.WriteLine(
                "Spacecraft continues normal cruise."
            );

            
            if (stage == 1)
            {
                PropulsionDamageChallenge();

                if (!status.missionAlive)
                {
                    return;
                }
            }

        
            if (stage == 2)
            {
                PowerProblemChallenge();

                if (!status.missionAlive)
                {
                    return;
                }
            }

            status.ShowStatus();
        }

        Console.WriteLine();
        Console.WriteLine(
            "Spacecraft has reached the destination region."
        );
    }


  
    private void PropulsionDamageChallenge()
    {
        Console.WriteLine();
        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "             EMERGENCY: PROPULSION"
        );

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine();

        Console.WriteLine(
            "WARNING!"
        );

        Console.WriteLine(
            "A sudden vibration has been detected."
        );

        Console.WriteLine(
            "One propulsion unit has been damaged."
        );

        Console.WriteLine(
            "The spacecraft can still operate."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Choose a response:"
        );

        Console.WriteLine(
            "1. Use the remaining propulsion system aggressively"
        );

        Console.WriteLine(
            "2. Perform a conservative course correction"
        );

        Console.WriteLine(
            "3. Ignore the problem and continue"
        );

        Console.Write("Choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        status.propulsionDamaged = true;

        if (choice == 1)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Remaining propulsion units are being used."
            );

            Console.WriteLine(
                "The spacecraft maintains its trajectory."
            );

            status.fuel -= 15;
            status.trajectoryDeviation += 2;

            Console.WriteLine(
                "Extra fuel consumed: 15%"
            );
        }
        else if (choice == 2)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Mission control performs a conservative correction."
            );

            status.fuel -= 7;
            status.trajectoryDeviation += 4;

            Console.WriteLine(
                "Fuel consumed: 7%"
            );

            Console.WriteLine(
                "Trajectory deviation increased slightly."
            );
        }
        else
        {
            Console.WriteLine();

            Console.WriteLine(
                "The damaged propulsion unit is left untouched."
            );

            status.fuel -= 2;
            status.trajectoryDeviation += 12;

            Console.WriteLine(
                "WARNING: Trajectory deviation is increasing."
            );
        }

        
        if (status.trajectoryDeviation >= 25)
        {
            Console.WriteLine();
            Console.WriteLine(
                "CRITICAL TRAJECTORY ERROR!"
            );

            Console.WriteLine(
                "The spacecraft can no longer reach the planned destination."
            );

            status.missionAlive = false;
            return;
        }

        if (status.fuel <= 0)
        {
            Console.WriteLine();
            Console.WriteLine(
                "CRITICAL: SPACECRAFT HAS RUN OUT OF FUEL."
            );

            status.missionAlive = false;
            return;
        }

        Console.WriteLine();
        Console.WriteLine(
            "Propulsion emergency handled."
        );
    }


         
    private void PowerProblemChallenge()
    {
        Console.WriteLine();
        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "             POWER SYSTEM WARNING"
        );

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine();

        Console.WriteLine(
            "Solar power generation has dropped."
        );

        Console.WriteLine(
            "The spacecraft is receiving less energy than expected."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Choose a response:"
        );

        Console.WriteLine(
            "1. Continue scientific systems normally"
        );

        Console.WriteLine(
            "2. Shut down non-essential systems"
        );

        Console.WriteLine(
            "3. Use battery reserves"
        );

        Console.Write("Choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            Console.WriteLine();

            Console.WriteLine(
                "All systems remain active."
            );

            status.battery -= 20;

            Console.WriteLine(
                "Battery consumption increased."
            );
        }
        else if (choice == 2)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Non-essential systems have been shut down."
            );

            status.battery -= 5;

            Console.WriteLine(
                "Battery consumption reduced."
            );
        }
        else
        {
            Console.WriteLine();

            Console.WriteLine(
                "Battery reserves are being used."
            );

            status.battery -= 12;

            status.scienceData += 5;
        }

        if (status.battery <= 0)
        {
            Console.WriteLine();
            Console.WriteLine(
                "CRITICAL: BATTERY COMPLETELY DEPLETED."
            );

            Console.WriteLine(
                "Spacecraft cannot continue normal operations."
            );

            status.missionAlive = false;
            return;
        }

        Console.WriteLine();
        Console.WriteLine(
            "Power problem successfully managed."
        );
    }


  
    private void DestinationOperations()
    {
        Console.WriteLine();
        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "           DESTINATION OPERATIONS"
        );

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine();

        if (missionName == "Dragonfly")
        {
            DragonflyLanding();
        }
        else if (missionName == "Psyche")
        {
            PsycheOrbit();
        }
        else if (missionName == "Europa Clipper")
        {
            EuropaFlyby();
        }
        else
        {
            EarthObservationMission();
        }

        if (!status.missionAlive)
        {
            return;
        }

        ScientificResearch();

        if (!status.missionAlive)
        {
            return;
        }

        DataManagement();
    }


   
    private void DragonflyLanding()
    {
        Console.WriteLine(
            "Dragonfly is beginning descent toward Titan."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Atmospheric conditions are being monitored."
        );

        Console.WriteLine(
            "Descent systems activated."
        );

        status.battery -= 8;
        status.fuel -= 5;

        Console.WriteLine(
            "Landing completed successfully."
        );
    }


    
    private void PsycheOrbit()
    {
        Console.WriteLine(
            "Spacecraft is entering the planned orbit around Psyche."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Orbit insertion maneuver initiated."
        );

        status.fuel -= 10;
        status.battery -= 6;

        status.trajectoryDeviation += 3;

        Console.WriteLine(
            "Psyche orbit established."
        );
    }


   
    private void EuropaFlyby()
    {
        Console.WriteLine(
            "Spacecraft is approaching Europa."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Preparing scientific instruments for flyby."
        );

        status.fuel -= 6;
        status.battery -= 7;

        Console.WriteLine(
            "Europa observation window opened."
        );
    }


   
    private void EarthObservationMission()
    {
        Console.WriteLine(
            "Spacecraft has reached its Earth observation orbit."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Observation instruments activated."
        );

        status.battery -= 5;

        Console.WriteLine(
            "Observation window started."
        );
    }


       private void ScientificResearch()
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== SCIENTIFIC RESEARCH ====="
        );

        Console.WriteLine();

        if (missionName == "Dragonfly")
        {
            SampleCollectionChallenge();
        }
        else
        {
            InstrumentFailureChallenge();
        }

        if (!status.missionAlive)
        {
            return;
        }

       
        status.scienceData += 35;
        status.dataStorage += 30;

        Console.WriteLine();

        Console.WriteLine(
            "Scientific operations produced valuable data."
        );

        Console.WriteLine(
            "Science data collected: " +
            status.scienceData.ToString("0.0") + "%"
        );
    }


    private void SampleCollectionChallenge()
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== SAMPLE COLLECTION CHALLENGE ====="
        );

        Console.WriteLine();

        Console.WriteLine(
            "The sampling mechanism has encountered resistance."
        );

        Console.WriteLine(
            "The first collection attempt was unsuccessful."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Choose an action:"
        );

        Console.WriteLine(
            "1. Try the sampling mechanism again"
        );

        Console.WriteLine(
            "2. Move to another sampling location"
        );

        Console.WriteLine(
            "3. Collect a smaller sample"
        );

        Console.Write("Choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Sampling mechanism attempts a second collection."
            );

            status.battery -= 15;
            status.dataStorage += 20;

            status.sampleCollected = true;

            Console.WriteLine(
                "Full sample successfully collected."
            );
        }
        else if (choice == 2)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Spacecraft moves to another location."
            );

            status.battery -= 10;
            status.fuel -= 5;
            status.dataStorage += 15;

            status.sampleCollected = true;

            Console.WriteLine(
                "Sample successfully collected."
            );
        }
        else
        {
            Console.WriteLine();

            Console.WriteLine(
                "A smaller sample is collected."
            );

            status.battery -= 5;
            status.dataStorage += 10;

            status.sampleCollected = true;
        }

        if (status.battery <= 0)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Battery depleted during sample collection."
            );

            status.missionAlive = false;
        }
    }


  
    private void InstrumentFailureChallenge()
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== SCIENTIFIC INSTRUMENT FAILURE ====="
        );

        Console.WriteLine();

        Console.WriteLine(
            "WARNING!"
        );

        Console.WriteLine(
            "The primary scientific instrument is malfunctioning."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Choose an action:"
        );

        Console.WriteLine(
            "1. Attempt instrument recovery"
        );

        Console.WriteLine(
            "2. Use backup/alternative observation mode"
        );

        Console.WriteLine(
            "3. Continue without repairing it"
        );

        Console.Write("Choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Instrument recovery procedure started."
            );

            status.battery -= 12;
            status.dataStorage += 25;

            status.instrumentDamaged = false;

            Console.WriteLine(
                "Instrument successfully recovered."
            );
        }
        else if (choice == 2)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Alternative instrument mode activated."
            );

            status.battery -= 8;
            status.dataStorage += 18;

            status.instrumentDamaged = true;
        }
        else
        {
            Console.WriteLine();

            Console.WriteLine(
                "Mission continues with reduced scientific capability."
            );

            status.dataStorage += 8;

            status.instrumentDamaged = true;
        }

        if (status.battery <= 0)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Battery depleted."
            );

            status.missionAlive = false;
        }
    }


    
    private void DataManagement()
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== DATA STORAGE MANAGEMENT ====="
        );

        if (status.dataStorage < 70)
        {
            Console.WriteLine(
                "Data storage is sufficient."
            );

            return;
        }

        Console.WriteLine();

        Console.WriteLine(
            "WARNING: Data storage is nearly full."
        );

        Console.WriteLine(
            "Current storage: " +
            status.dataStorage.ToString("0.0") + "%"
        );

        Console.WriteLine();

        Console.WriteLine(
            "Choose an action:"
        );

        Console.WriteLine(
            "1. Transmit data to Earth"
        );

        Console.WriteLine(
            "2. Delete low-priority data"
        );

        Console.WriteLine(
            "3. Stop collecting additional data"
        );

        Console.Write("Choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            Console.WriteLine();

            Console.WriteLine(
                "High-gain communication system activated."
            );

            Console.WriteLine(
                "Transmitting scientific data to Earth..."
            );

            status.battery -= 15;

            status.dataStorage -= 45;

            Console.WriteLine(
                "Data transmission completed."
            );
        }
        else if (choice == 2)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Low-priority data deleted."
            );

            status.dataStorage -= 30;
        }
        else
        {
            Console.WriteLine();

            Console.WriteLine(
                "Additional scientific data collection stopped."
            );

            Console.WriteLine(
                "Existing data preserved."
            );
        }

        if (status.battery <= 0)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Battery is too low for normal operation."
            );

            status.missionAlive = false;
        }
    }


    
    private void ReturnJourney()
    {
        Console.WriteLine();
        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "             RETURN JOURNEY"
        );

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine();

        Console.WriteLine(
            "Scientific objective has been completed."
        );

        if (missionName == "Dragonfly")
        {
            Console.WriteLine(
                "Sample collected: " +
                (status.sampleCollected ? "YES" : "NO")
            );
        }

        Console.WriteLine();

        Console.WriteLine(
            "Spacecraft is preparing to leave the destination."
        );

        status.fuel -= 8;
        status.battery -= 5;

        /*
         * Challenge 4:
         * damaged propulsion during return.
         */

        ReturnPropulsionFailure();

        if (!status.missionAlive)
        {
            return;
        }

        /*
         * Challenge 5:
         * low fuel.
         */

        LowFuelChallenge();

        if (!status.missionAlive)
        {
            return;
        }

        
        for (int stage = 1; stage <= 3; stage++)
        {
            Console.WriteLine();

            Console.WriteLine(
                "--- Return Cruise Stage " +
                stage +
                " ---"
            );

            status.distance -= 15;
            status.fuel -= 4;
            status.battery -= 3;

            if (status.fuel <= 0)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "CRITICAL: FUEL EXHAUSTED DURING RETURN."
                );

                status.missionAlive = false;
                return;
            }

            status.ShowStatus();
        }

        Console.WriteLine();

        Console.WriteLine(
            "Earth is now within operational range."
        );
    }


        private void ReturnPropulsionFailure()
    {
        Console.WriteLine();
        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "       EMERGENCY DURING RETURN JOURNEY"
        );

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine();

        Console.WriteLine(
            "WARNING!"
        );

        Console.WriteLine(
            "A propulsion unit has suffered structural damage."
        );

        Console.WriteLine(
            "The damaged unit cannot operate at full capacity."
        );

        Console.WriteLine();

        Console.WriteLine(
            "Choose a response:"
        );

        Console.WriteLine(
            "1. Use remaining propulsion units"
        );

        Console.WriteLine(
            "2. Perform a slow trajectory correction"
        );

        Console.WriteLine(
            "3. Continue without correction"
        );

        Console.Write("Choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Remaining propulsion units activated."
            );

            status.fuel -= 15;
            status.trajectoryDeviation += 2;

            Console.WriteLine(
                "Higher fuel consumption detected."
            );
        }
        else if (choice == 2)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Slow correction maneuver initiated."
            );

            status.fuel -= 7;
            status.trajectoryDeviation += 5;

            Console.WriteLine(
                "Return trajectory remains controllable."
            );
        }
        else
        {
            Console.WriteLine();

            Console.WriteLine(
                "No correction performed."
            );

            status.trajectoryDeviation += 15;

            Console.WriteLine(
                "Trajectory deviation has increased."
            );
        }

        if (status.trajectoryDeviation >= 30)
        {
            Console.WriteLine();
            Console.WriteLine(
                "CRITICAL!"
            );

            Console.WriteLine(
                "Spacecraft has deviated too far from the return trajectory."
            );

            status.missionAlive = false;
            return;
        }

        if (status.fuel <= 0)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Fuel exhausted."
            );

            status.missionAlive = false;
            return;
        }

        Console.WriteLine();
        Console.WriteLine(
            "Return propulsion emergency handled."
        );
    }


   
    private void LowFuelChallenge()
    {
        Console.WriteLine();
        Console.WriteLine(
            "===== LOW FUEL WARNING ====="
        );

        if (status.fuel > 30)
        {
            Console.WriteLine(
                "Fuel level is currently sufficient."
            );

            return;
        }

        Console.WriteLine();

        Console.WriteLine(
            "WARNING: Remaining fuel is low."
        );

        Console.WriteLine(
            "Remaining fuel: " +
            status.fuel.ToString("0.0") +
            "%"
        );

        Console.WriteLine();

        Console.WriteLine(
            "Choose a strategy:"
        );

        Console.WriteLine(
            "1. Preserve fuel and use a longer trajectory"
        );

        Console.WriteLine(
            "2. Use fuel for a precise correction"
        );

        Console.WriteLine(
            "3. Attempt fastest possible return"
        );

        Console.Write("Choice: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Fuel-saving trajectory selected."
            );

            status.fuel -= 3;
            status.trajectoryDeviation += 5;
        }
        else if (choice == 2)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Precision correction performed."
            );

            status.fuel -= 12;
            status.trajectoryDeviation -= 3;

            if (status.trajectoryDeviation < 0)
            {
                status.trajectoryDeviation = 0;
            }
        }
        else
        {
            Console.WriteLine();

            Console.WriteLine(
                "High-energy return maneuver selected."
            );

            status.fuel -= 18;
            status.battery -= 8;
        }

        if (status.fuel <= 0)
        {
            Console.WriteLine();

            Console.WriteLine(
                "CRITICAL: NO FUEL REMAINS."
            );

            Console.WriteLine(
                "Spacecraft cannot perform further propulsion maneuvers."
            );

            status.missionAlive = false;
            return;
        }
    }


   
    private void EarthArrival()
    {
        Console.WriteLine();
        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "                EARTH ARRIVAL"
        );

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine();

        Console.WriteLine(
            "Spacecraft has reached Earth's operational region."
        );

        Console.WriteLine();

        if (missionName == "Dragonfly")
        {
            Console.WriteLine(
                "Mission vehicle remains on Titan."
            );

            Console.WriteLine(
                "Scientific data and mission results are being transmitted to Earth."
            );

            FinalCommunication();
        }
        else
        {
            Console.WriteLine(
                "Preparing final communication with Earth."
            );

            FinalCommunication();
        }
    }


       private void FinalCommunication()
    {
        Console.WriteLine();

        if (status.communicationDamaged)
        {
            Console.WriteLine(
                "Communication system is damaged."
            );

            Console.WriteLine(
                "Using backup communication mode."
            );

            status.battery -= 10;
        }
        else
        {
            Console.WriteLine(
                "Primary communication system is operational."
            );

            status.battery -= 5;
        }

        if (status.battery <= 0)
        {
            Console.WriteLine(
                "Communication interrupted due to battery depletion."
            );

            status.missionAlive = false;
            return;
        }

        Console.WriteLine();

        Console.WriteLine(
            "Scientific data successfully received by Earth."
        );
    }


   
    private void FinalReport()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "              FINAL MISSION REPORT"
        );

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine();

        Console.WriteLine(
            "Mission: " + missionName
        );

        Console.WriteLine(
            "Destination: " +
            trajectory.destination
        );

        Console.WriteLine();

        if (!status.missionAlive)
        {
            Console.WriteLine(
                "MISSION STATUS: FAILED"
            );

            Console.WriteLine(
                "The spacecraft could not complete the mission."
            );
        }
        else
        {
            status.researchCompleted = true;

            Console.WriteLine(
                "MISSION STATUS: SUCCESSFUL"
            );

            Console.WriteLine(
                "Scientific objective completed."
            );

            if (missionName == "Dragonfly")
            {
                Console.WriteLine(
                    "Sample collected: " +
                    (status.sampleCollected ? "YES" : "NO")
                );
            }

            Console.WriteLine(
                "Scientific data collected: " +
                status.scienceData.ToString("0.0") +
                "%"
            );

            Console.WriteLine(
                "Scientific data transmitted to Earth."
            );
        }

        Console.WriteLine();

        Console.WriteLine(
            "Final Fuel: " +
            status.fuel.ToString("0.0") +
            "%"
        );

        Console.WriteLine(
            "Final Battery: " +
            status.battery.ToString("0.0") +
            "%"
        );

        Console.WriteLine(
            "Final Trajectory Deviation: " +
            status.trajectoryDeviation.ToString("0.0") +
            "%"
        );

        Console.WriteLine();

        Console.WriteLine(
            "================================================"
        );

        Console.WriteLine(
            "              SIMULATION COMPLETE"
        );

        Console.WriteLine(
            "================================================"
        );
    }
}


public class mycode
{
    public static void Main(string[] args)
    {
        Console.WriteLine(
            "===== SPACE MISSION DESIGN SIMULATOR ====="
        );

        Console.WriteLine();

        Console.Write("Please Enter your Name: ");
        Userprofile.name = Console.ReadLine();

        Console.Write("Please Enter your Age: ");
        string age = Console.ReadLine();

        Console.WriteLine();

        Console.WriteLine(
            "Welcome " + Userprofile.name + "!"
        );


       
        string[] missionNames =
            AIresponse.GetMissionName();

        string[] missionObjectives =
            AIresponse.GetMissionObjective();

        string[] missionObstacles =
            AIresponse.GetMissionObstacle();

        Console.WriteLine();
        Console.WriteLine(
            "===== MISSION LIBRARY ====="
        );

        for (int i = 0;
             i < missionNames.Length;
             i++)
        {
            Console.WriteLine(
                (i + 1) + ". " + missionNames[i]
            );
        }

        Console.WriteLine();
        Console.Write("Choose a mission: ");

        int missionChoice =
            Convert.ToInt32(Console.ReadLine()) - 1;

        if (missionChoice < 0 ||
            missionChoice >= missionNames.Length)
        {
            Console.WriteLine("Invalid mission.");
            return;
        }

        string selectedMission =
            missionNames[missionChoice];

        string selectedObjective =
            missionObjectives[missionChoice];

        string selectedObstacle =
            missionObstacles[missionChoice];

        Console.WriteLine();

        Console.WriteLine(
            "Selected Mission: " +
            selectedMission
        );

        Console.WriteLine(
            "Mission Objective: " +
            selectedObjective
        );

        Console.WriteLine();

        Console.WriteLine(
            "Mission Problem / Obstacle: " +
            selectedObstacle
        );


      
        Console.WriteLine();
        Console.WriteLine(
            "===== ORGANIZER BUDGET ====="
        );

        AIresponse.GetBudgetFromTheOrganizer(1000);

        Console.WriteLine(
            "Your Mission Budget: $" +
            Userprofile.budget + "M"
        );


        

        LaunchVehicle launchVehicle =
            new LaunchVehicle();

        if (!launchVehicle.ChooseLaunchVehicle())
        {
            return;
        }


       
        SpaceCraft spacecraft =
            new SpaceCraft();

        if (!spacecraft.DesignMandatorySystems(
            selectedObjective))
        {
            return;
        }


       
        Communication communication =
            new Communication();

        if (!communication.ChooseCommunication(
            spacecraft,
            selectedMission))
        {
            return;
        }


               Console.WriteLine();
        Console.WriteLine(
            "===== SCIENTIFIC INSTRUMENTS ====="
        );

        string[] instrumentLibrary =
        {
            "Cassini Imaging Science Subsystem — Camera",
            "Composite Infrared Spectrometer",
            "Ultraviolet Imaging Spectrograph",
            "Visible and Infrared Mapping Spectrometer",
            "Cassini Plasma Spectrometer",
            "Cosmic Dust Analyzer",
            "Ion and Neutral Mass Spectrometer",
            "Dual-Technique Magnetometer",
            "Magnetospheric Imaging Instrument",
            "Radio and Plasma Wave Science",
            "Cassini Radar",
            "Radio Science Subsystem",
            "High Resolution Imaging Science Experiment",
            "Context Camera",
            "Mars Color Imager",
            "Compact Reconnaissance Imaging Spectrometer for Mars",
            "Mars Climate Sounder",
            "Mastcam-Z",
            "SuperCam",
            "SHERLOC"
        };

        string[] purpose =
        {
            "Take visible and near-infrared images.",
            "Measure infrared radiation.",
            "Analyze ultraviolet light.",
            "Map objects using visible and infrared wavelengths.",
            "Measure plasma and charged particles.",
            "Detect and analyze dust particles.",
            "Analyze neutral gases and ions.",
            "Measure magnetic-field strength and direction.",
            "Study energetic particles.",
            "Measure electric and magnetic waves.",
            "Image and study planetary surfaces.",
            "Investigate gravity and planetary properties.",
            "Take extremely high-resolution images.",
            "Provide wide-area surface images.",
            "Monitor planetary weather and dust.",
            "Identify minerals and mineral signatures.",
            "Measure atmospheric conditions.",
            "Take high-definition color images.",
            "Analyze rocks and soil.",
            "Detect minerals, organic molecules and potential biosignatures."
        };

        int[] costs =
        {
            40, 45, 35, 45, 30,
            30, 25, 20, 30, 25,
            50, 35, 60, 20, 15,
            45, 25, 30, 40, 40
        };

        double[] mass =
        {
            57.83, 39.24, 14.46, 37.14, 12.50,
            16.36, 9.25, 3.00, 16.00, 6.80,
            41.43, 14.38, 65.00, 3.00, 0.481,
            32.92, 9.00, 4.00, 10.60, 4.72
        };

        double[] power =
        {
            50.90, 32.89, 11.83, 27.20, 14.50,
            18.38, 27.70, 3.10, 14.00, 7.00,
            108.40, 80.70, 60.00, 7.00, 5.00,
            16.00, 11.00, 17.40, 17.90, 48.80
        };

        string[] powerDisplay =
        {
            "50.90 W",
            "32.89 W",
            "11.83 W",
            "27.20 W",
            "14.50 W",
            "18.38 W",
            "27.70 W",
            "3.10 W",
            "14 W",
            "7.00 W",
            "108.40 W",
            "80.70 W",
            "60 W",
            "7 W imaging / 5 W idle",
            "<5 W",
            "~16 W",
            "11 W",
            "17.4 W",
            "17.9 W",
            "48.8 W"
        };

        string[] data =
        {
            "365.568 kbps",
            "6.000 kbps",
            "32.096 kbps",
            "182.784 kbps",
            "0.5–16 kbps",
            "0.524 kbps",
            "1.50 kbps",
            "3.60 kbps",
            "7 kbps",
            "0.90 kbps",
            "364.800 kbps",
            "Not applicable",
            "28 Gbit",
            "256 MB onboard DRAM buffer",
            "6.2 Gbit",
            "2–2.5 Gbit",
            "2-second signal integration every 34 seconds",
            "148 Mbit",
            "15.5 Mbit",
            "79.7 Mbit"
        };

        string[] suitableFor =
        {
            "Imaging",
            "Infrared spectroscopy",
            "Ultraviolet spectroscopy",
            "Mapping and composition",
            "Plasma analysis",
            "Dust analysis",
            "Atmospheric composition",
            "Magnetic field studies",
            "Magnetosphere studies",
            "Radio and plasma wave studies",
            "Surface and subsurface imaging",
            "Radio science",
            "High-resolution surface imaging",
            "Surface imaging",
            "Atmospheric monitoring",
            "Mineral and ice mapping",
            "Atmospheric profiling",
            "Surface imaging",
            "Rock and mineral composition",
            "Organic and mineral detection"
        };

        Instruments.GetTheInstrumentTools(
            instrumentLibrary,
            purpose,
            costs,
            mass,
            power,
            powerDisplay,
            data,
            suitableFor,
            spacecraft
        );


               spacecraft.ChooseOptionalSystems();


              Console.WriteLine();
        Console.WriteLine(
            "===== FINAL MISSION CHECK ====="
        );

        double spacecraftMass =
            373.0 - spacecraft.massCapacity;

        Console.WriteLine(
            "Spacecraft Mass: " +
            spacecraftMass + " kg"
        );

        Console.WriteLine(
            "Remaining Power Capacity: " +
            spacecraft.powerCapacity + " W"
        );

        Console.WriteLine(
            "Remaining Budget: $" +
            Userprofile.budget + "M"
        );

        if (!launchVehicle.CheckPayload(
            spacecraftMass))
        {
            Console.WriteLine();
            Console.WriteLine(
                "Mission cannot launch."
            );

            return;
        }


       
        OrbitalConstraints orbit =
            new OrbitalConstraints();

        orbit.ChooseOrbit(selectedMission);


               MissionTrajectory trajectory =
            new MissionTrajectory();

        trajectory.missionName =
            selectedMission;

        trajectory.objective =
            selectedObjective;


       

        if (selectedMission == "Europa Clipper")
        {
            trajectory.destination =
                "Europa / Jupiter System";

            trajectory.trajectory1 =
                "Jupiter transfer trajectory";

            trajectory.trajectory2 =
                "Alternative lower-energy transfer";
        }
        else if (selectedMission == "Psyche")
        {
            trajectory.destination =
                "Asteroid Psyche";

            trajectory.trajectory1 =
                "Interplanetary transfer to Psyche";

            trajectory.trajectory2 =
                "Alternative fuel-saving transfer";
        }
        else if (selectedMission == "Dragonfly")
        {
            trajectory.destination =
                "Titan";

            trajectory.trajectory1 =
                "Direct interplanetary transfer to Titan";

            trajectory.trajectory2 =
                "Fuel-saving transfer with longer cruise";
        }
        else if (selectedMission == "NISAR")
        {
            trajectory.destination =
                "Earth Observation Orbit";

            trajectory.trajectory1 =
                "Planned Earth observation orbit";

            trajectory.trajectory2 =
                "Alternative observation orbit";
        }
        else
        {
            trajectory.destination =
                "Earth Survey Orbit";

            trajectory.trajectory1 =
                "Sun-synchronous survey orbit";

            trajectory.trajectory2 =
                "Alternative survey orbit";
        }

        trajectory.ShowTrajectory();


       
        Console.WriteLine();
        Console.WriteLine(
            "===== MISSION READY FOR LAUNCH ====="
        );

        Console.WriteLine(
            "Mission: " + selectedMission
        );

        Console.WriteLine(
            "Launch Vehicle: " +
            launchVehicle.name[
                launchVehicle.selectedVehicle
            ]
        );

        Console.WriteLine(
            "Final Budget: $" +
            Userprofile.budget + "M"
        );

        Console.WriteLine();

        Console.WriteLine(
            "MISSION LAUNCH SUCCESSFUL!"
        );


       
        MissionSimulation simulation =
            new MissionSimulation(
                selectedMission,
                selectedObjective,
                spacecraft,
                trajectory
            );

        simulation.RunSimulation();
    }
}
