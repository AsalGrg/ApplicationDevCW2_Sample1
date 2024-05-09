export default function formatDate(input) {
    // Create a new Date object from the input string
    const date = new Date(input);

    // Define arrays for month names and AM/PM indicators
    const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    const ampm = ["AM", "PM"];

    // Extract date components
    const month = months[date.getMonth()];
    const day = date.getDate();
    const year = date.getFullYear();
    let hours = date.getHours();
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const ampmIndicator = ampm[hours >= 12 ? 1 : 0];

    // Convert hours to 12-hour format
    if (hours > 12) {
        hours -= 12;
    }

    // Format the date string
    const formattedDate = `${month} ${day}, ${year} at ${hours}:${minutes} ${ampmIndicator}`;

    return formattedDate;
}