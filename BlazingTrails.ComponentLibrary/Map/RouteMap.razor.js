// Initializes our module with a Leaflet map and connects events with our 
// C# component using its reference. If we supply existing waypoints, they
// are drawn on the map, and the map centers on these.
export function initialize(hostElement, routeMapComponent, existingWaypoints, isReadOnly) {
  // Initialize Leaflet (L)
  hostElement.map = L.map(hostElement).setView([51.7, 0.1], 7);
  L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
    attribution:
      '&copy;<a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    maxZoom: 18,
    opacity: 0.75,
  }).addTo(hostElement.map);

  hostElement.waypoints = [];
  hostElement.lines = [];
  // Add existing waypoints, if they exist
  existingWaypoints?.forEach(addWaypoint);

  // Center and zoom into the map to show the known waypoints 
  if (hostElement.waypoints.length > 0) {
    let waypointsGroup = new L.featureGroup(hostElement.waypoints);
    hostElement.map.fitBounds(waypointsGroup.getBounds().pad(1));
  }

  if (!isReadOnly) {
    hostElement.map.on("click", (e) => {
      addWaypoint(e.latlng);
      routeMapComponent.invokeMethodAsync("WaypointAdded", e.latlng.lat, e.latlng.lng);
    });
  }

  // Adds a marker to the map that represents the waypoint. Markers are 
  // then connected with a polyline
  function addWaypoint(pos) {
    let waypoint = L.marker(pos);
    waypoint.addTo(hostElement.map);
    hostElement.waypoints.push(waypoint);

    let line = L.polyline(
      hostElement.waypoints.map((m) => m.getLatLng()),
      { color: "var(--brand)" }
    ).addTo(hostElement.map);

    hostElement.lines.push(line);
  }
}

// Removes the last waypoint by removing the last marker 
// and line segment from the map. Returns its coordinates
export function deleteLastWaypoint(hostElement) {
  if (hostElement.waypoints.length > 0) {
    let lastWaypoint = hostElement.waypoints.pop();
    hostElement.map.removeLayer(lastWaypoint);

    if (hostElement.lines.length > 0) {
      let lastLine = hostElement.lines.pop();
      lastLine.remove(hostElement.map);

      let pos = lastWaypoint.getLatLng();
      return { "Lat": pos.lat, "Lng": pos.lng };
    }
  }
}