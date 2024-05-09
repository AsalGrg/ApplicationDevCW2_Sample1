import { Button, Select, Text } from "@mantine/core";
import { MonthPicker, MonthPickerInput } from "@mantine/dates";
import React, { useEffect, useState } from "react";
import { MdCancel } from "react-icons/md";
import { get_analysis_data } from "../../services/getAnalysisData";
import EachBlogCard from "../../components/global/EachBlogCard";

const AdminHome = () => {
  const [selectedDate, setSelectedDate] = useState("All Time");
  const [selectedDateCalendar, setselectedDateCalendar] = useState("");

  const [analysisDetails, setanalysisDetails] = useState({
    TotalPosts: 0,
    TotalUpvotes: 0,
    TotalDownvotes: 0,
    TotalComments: 0,
    TopBlogs: [],
  });
  useEffect(() => {
    async function getAllAnalyis() {
      const analysisData = {
        SelectedDateOption: selectedDate,
        SelectedCalendarDate: selectedDateCalendar,
      };
      const res = await get_analysis_data(analysisData);

      if (res.ok) {
        const data = await res.json();
        setanalysisDetails(data);
      }
    }

    getAllAnalyis();
  }, [selectedDate, selectedDateCalendar]);

  console.log(selectedDateCalendar);
  return (
    <div>
      <Text size="20px" fw={700}>
        Dashboard
      </Text>

      <div className="w-25 mt-4">
        {selectedDate !== "Select Date" ? (
          // Render Select component if selectedDate is defined
          <Select
            label="Pick a date"
            className="mt-4"
            size="sm"
            placeholder="Pick a date"
            value={selectedDate}
            data={["All Time", "Select Date"]} // Include selected dates in the data prop
            onChange={(value) => {
              setSelectedDate(value);
            }}
          />
        ) : (
          // Render MonthPicker component if selectedDate is not defined
          <div className="d-flex gap-2 align-items-center">
            <input
              type="date"
              placeholder="Pick date"
              value={selectedDateCalendar}
              onChange={(date) => {
                setselectedDateCalendar(date.target.value);
              }}
            />
            <Button
              size="xs"
              className=""
              onClick={() => setSelectedDate("All Time")}
            >
              <MdCancel />
            </Button>
          </div>
        )}
      </div>

      <div className="mt-4 d-flex gap-5">
        <div
          className="col-3 border border-2 rounded d-flex justify-content-between align-items-center px-4"
          style={{
            height: "80px",
            width: "200px",
          }}
        >
          <Text fw={600}>Posts</Text>
          <Text fw={700}>{analysisDetails.TotalPosts}</Text>
        </div>

        <div
          className="col-3 border border-2 rounded d-flex justify-content-between align-items-center px-4"
          style={{
            height: "80px",
            width: "200px",
          }}
        >
          <Text fw={600}>Upvotes</Text>
          <Text fw={700}>{analysisDetails.TotalUpvotes}</Text>
        </div>

        <div
          className="col-3 border border-2 rounded d-flex justify-content-between align-items-center px-4"
          style={{
            height: "80px",
            width: "200px",
          }}
        >
          <Text fw={600}>Downvotes</Text>
          <Text fw={700}>{analysisDetails.TotalDownvotes}</Text>
        </div>

        <div
          className="col-3 border border-2 rounded d-flex justify-content-between align-items-center px-4"
          style={{
            height: "80px",
            width: "200px",
          }}
        >
          <Text fw={600}>Comments</Text>
          <Text fw={700}>{analysisDetails.TotalComments}</Text>
        </div>
      </div>

      <div className="mt-5">
        <Text fw={700} size="md">
          Top blogs
        </Text>

        <div className="d-flex flex-column gap-5">
        {analysisDetails.TopBlogs.map((each) => (
          <EachBlogCard blog={each} />
        ))}
        </div>
      </div>
    </div>
  );
};

export default AdminHome;
