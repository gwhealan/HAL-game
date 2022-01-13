HAL-game
--
Project for CIS 407 - Seminar Career / Internship

Description
--
Take control of a rouge AI on a space station and try to kill off the crewmembers, something like in the movie 2001: A Space Odyssey. 

Dev Log
--
1/12/2022 - Created basic camera controls.

	TODO: Camera bound currently a box, therefore if the user zooms out at a box corner, user can see past expected distance
		- Solution: Create a colision mesh using a square pyramid. Use Collider.ClosestPoint to restrict.
	TODO: Crew Movement
		- Pathing
		- Scheduler
	TODO: AI (user) interactable
	TODO: UI (ohhhhhh man)
		- Start Screen
			- Start
			- Settings / Controls
		- In Game UI
		- ...

Summary: Much to do but solid start. Plan to finish basic controls, crew movement, and simple scheduler by week 5
