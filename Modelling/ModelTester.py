import tkinter as tk
from tkinter import ttk
from tkinter import messagebox
import matplotlib.pyplot as plt
import json
import numpy as np


class Application:  
    def __init__(self, parent):
        self.parent = parent
        self.parent.resizable(False, False)
        self.build_form()

    def load_data(self):
        try:
            data = {"leds":{}, "photos":{}, "surfaces":{}}            
            with open("data.json", "r") as read_file:
                data = json.load(read_file)
        except FileNotFoundError:
            messagebox.showerror("Error","Data file not found")            
        except Exception:
            messagebox.showerror("Error","Failed to load data")
        return data["leds"], data["photos"], data["surfaces"]        

    def validate_data(self):
        self.selected_led = self.leds[self.ledBox.get()]
        self.selected_photo = self.photos[self.photobox.get()]
        self.selected_surface_koef = self.surfaces[self.surphaceBox.get()]
        self.minheight = int(self.heightminbox.get())
        self.maxheight = int(self.heightmaxbox.get())
        self.stepheight = int(self.heightstepbox.get())
        self.width = int(self.widthbox.get())
        if (self.minheight > self.maxheight):
            self.maxheight = self.minheight
        if ((self.maxheight - self.minheight) < self.stepheight):
            self.stepheight = (self.maxheight - self.minheight) + 1
        if (self.width < self.selected_photo["width"]):
            self.width = self.selected_photo["width"]

    
    def find_B(self, h, w, alpha):
        a = np.radians(alpha)
        ca = np.cos(a)
        sa = np.sin(a)
        B = np.arccos((h*ca**2-h*sa**2+h+2*w*sa*ca)/(np.sqrt(h**2*sa**4-2*h**2*sa**2+h**2*ca**4+2*h**2*ca**2+2*h**2*sa**2*ca**2+h**2+4*h*w*sa*ca+w**2*sa**4+w**2*ca**4+2*w**2*sa**2*ca**2)))
        return(np.degrees(B))


    def find_kappa(self, alpha, betta):
        return(2*alpha + betta)


    def find_koef(self, angle, mapping, accuracy = 0):
        a = str(int(round(angle, accuracy)))
        if a in mapping.keys():
            return(mapping[a])
        else:
            return(0)


    def count_hitting_energy(self, h, w, p_len, alpha, led_rays, ray_energy, b_map, k_map):
        #find max and min bettas for rays that hit photodiode
        max_betta = self.find_B(h, w+p_len/2, alpha)
        min_betta = self.find_B(h, w-p_len/2, alpha)        
        
        #find rays that hit diapasone
        hitting_rays = 0
        for ray in led_rays:
            if (max_betta > ray > min_betta):
                emitting_energy = self.find_koef(ray, b_map) * ray_energy                
                reflected_energy = emitting_energy * self.selected_surface_koef
                kappa = self.find_kappa(alpha, ray)
                receiving_energy = self.find_koef(kappa, k_map) * reflected_energy
                hitting_rays += receiving_energy
        return(hitting_rays)
            

    def count(self):
        self.validate_data()
        heights = np.linspace(self.minheight, self.maxheight, self.stepheight)
        alpha_steps = 100
        alpha_angle_arange = 30 #total degrees
        alphas = [alpha_angle_arange*(i/alpha_steps)-(alpha_angle_arange/2) for i in range(alpha_steps+1)] #-15..+15 degrees

        led_steps = 9000
        led_angle_arange = 180
        led_rays = [led_angle_arange*(i/led_steps)-(led_angle_arange/2) for i in range(led_steps+1)]

        energy = 0.1

        #Angle VS Energy graph on different heights
        energies_ave = []
        names = []        
        for i in range(self.stepheight):
            names.append("h="+str(round(heights[i],2)))
            energies_ave.append(alphas)
            energies_ave.append([self.count_hitting_energy(heights[i], self.width, self.selected_photo["width"], a, led_rays, energy, self.selected_led["angles"], self.selected_photo["angles"]) for a in alphas])        
        plt.subplot(1, 2, 1)
        plt.plot(*energies_ave)        
        plt.gca().legend(names)
        plt.xlabel('Angle')
        plt.ylabel('Energy')        
        plt.title('Energy vs angle')

        #Height VS Energy graph on 0 degree
        if (self.stepheight < 5 or (self.maxheight - self.minheight) < 10):
            heights = np.linspace(0, 20, 20)
        energies_hve = [self.count_hitting_energy(h, self.width, self.selected_photo["width"], 0, led_rays, energy, self.selected_led["angles"], self.selected_photo["angles"]) for h in heights]
        plt.subplot(1, 2, 2)
        plt.plot(heights, energies_hve)
        plt.xlabel('Height, mm')
        plt.ylabel('Energy')        
        plt.title('Energy vs height')
        plt.show()

    def build_form(self):        
        #row 0
        tk.Label(text="Select LED:").grid(row=0, column=0, columnspan=2)
        tk.Label(text="Select Photodiode:").grid(row=0, column=2, columnspan=2)

        #row 1
        self.leds, self.photos, self.surfaces = self.load_data()        
        
        self.ledBox = ttk.Combobox(self.parent, state="readonly", values=list(self.leds.keys()))
        self.ledBox.current(0)
        self.ledBox.grid(row=1, column=0, columnspan=2, padx=(8,2), pady=2)

        self.photobox = ttk.Combobox(self.parent, state="readonly", values=list(self.photos.keys()))
        self.photobox.current(0)
        self.photobox.grid(row=1, column=2, columnspan=2, padx=(2,8), pady=2)

        #row 2
        tk.Label(text="Height, mm:").grid(row=2, column=0, columnspan=2)
        tk.Label(text="Select surface:").grid(row=2, column=2, columnspan=2)

        #row 3
        tk.Label(text="Min:").grid(row=3, column=0)        
        self.heightminbox = tk.Spinbox(state="readonly", width=5, from_=1, to=100)
        self.heightminbox.grid(row=3, column=1)

        self.surphaceBox = ttk.Combobox(self.parent, state="readonly", values=list(self.surfaces.keys()))
        self.surphaceBox.current(0)
        self.surphaceBox.grid(row=3, column=2, columnspan=2, padx=(0,5), pady=2)

        #row 4
        tk.Label(text="Max:").grid(row=4, column=0)        
        self.heightmaxbox = tk.Spinbox(state="readonly", width=5, from_=1, to=100)
        self.heightmaxbox.grid(row=4, column=1)
        tk.Label(text="Width, mm:").grid(row=4, column=2)        
        self.widthbox = tk.Spinbox(state="readonly", width=5, from_=1, to=100)
        self.widthbox.grid(row=4, column=3)

        #row 5
        tk.Label(text="Steps:").grid(row=5, column=0)        
        self.heightstepbox = tk.Spinbox(state="readonly", width=5, from_=1, to=100)
        self.heightstepbox.grid(row=5, column=1)

        #row 6
        self.countbutton = tk.Button(self.parent, text="Count", command=self.count)
        self.countbutton.grid(row=6, column=3, padx=4, pady=4)

if __name__ == "__main__":
    root = tk.Tk()
    root.title("Energy counter")
    app = Application(root)
    root.mainloop()